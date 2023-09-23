using System.Reflection;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Xunit.Sdk;

namespace AutoFixture.Attributes;

public class SetupAutoDataAttribute : DataAttribute
{
    private Lazy<IFixture> _fixture;

    public SetupAutoDataAttribute() : this(() => new Fixture())
    {
    }

    protected SetupAutoDataAttribute(Func<IFixture> fixtureFactory)
    {
        _fixture = fixtureFactory != null
            ? new Lazy<IFixture>(fixtureFactory, LazyThreadSafetyMode.PublicationOnly)
            : throw new ArgumentNullException(nameof(fixtureFactory));
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod);

        var setupMethodFound = TryFindAndValidateSetupMethod(testMethod, out var setupMethod);
        if (setupMethodFound)
        {
            ApplySetupFixtureCustomizations(setupMethod);
        }

        var objectList = new List<object>();
        foreach (var parameter in testMethod.GetParameters())
        {
            CustomizeFixture(parameter);
            var obj = Resolve(parameter);
            objectList.Add(obj);
        }

        return new[]
        {
            objectList.ToArray()
        };
    }

    private static bool TryFindAndValidateSetupMethod(MemberInfo testMethod, out MethodInfo? setupMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod.DeclaringType);

        setupMethod = testMethod.DeclaringType
            .GetMethods()
            .SingleOrDefault(m => m.GetCustomAttributes<SetupFixtureAttribute>().Any());
        if (setupMethod is not null && !setupMethod.IsStatic)
        {
            throw new ArgumentException($"{nameof(SetupFixtureAttribute)} must be used on a static method");
        }

        if (setupMethod is not null && setupMethod.ReturnType != typeof(IFixture))
        {
            throw new ArgumentException(
                $"{nameof(SetupFixtureAttribute)} must be used on a method which returns {nameof(IFixture)}");
        }

        return setupMethod is not null;
    }

    private void ApplySetupFixtureCustomizations(MethodInfo? setupMethod)
    {
        if (setupMethod is not null)
        {
            _fixture = new Lazy<IFixture>((IFixture)setupMethod.Invoke(null, new object?[] { _fixture.Value })!);
        }
    }

    private void CustomizeFixture(ParameterInfo p)
    {
        foreach (var customizationSource in p
                     .GetCustomAttributes().OfType<IParameterCustomizationSource>()
                     .OrderBy<IParameterCustomizationSource, IParameterCustomizationSource>(x => x,
                         new CustomizeAttributeComparer()))
            _fixture.Value.Customize(customizationSource.GetCustomization(p));
    }

    private object Resolve(ParameterInfo p)
    {
        return new SpecimenContext(_fixture.Value).Resolve(p);
    }

    private class CustomizeAttributeComparer : Comparer<IParameterCustomizationSource>
    {
        public override int Compare(IParameterCustomizationSource? left, IParameterCustomizationSource? right)
        {
            var leftFrozen = left is FrozenAttribute;
            var rightFrozen = right is FrozenAttribute;
            if (leftFrozen && !rightFrozen)
            {
                return 1;
            }

            return rightFrozen && !leftFrozen ? -1 : 0;
        }
    }
}