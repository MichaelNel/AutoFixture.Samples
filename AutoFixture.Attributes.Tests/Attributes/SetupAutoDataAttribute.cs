using System.Reflection;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Xunit.Sdk;

namespace AutoFixture.Attributes.Tests.Attributes;

public class SetupAutoDataAttribute : DataAttribute
{
    private readonly Lazy<IFixture> _fixture;

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

        var setupMethod = FindAndValidateSetupMethod(testMethod);
        if (setupMethod is not null)
        {
            _fixture.Value.Customize(new SetupFixtureCustomizationWrapper(setupMethod));
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

    private static MethodInfo? FindAndValidateSetupMethod(MemberInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod.DeclaringType);

        var setupMethod = testMethod.DeclaringType
            .GetMethods()
            .SingleOrDefault(m => m.GetCustomAttributes<SetupFixtureAttribute>().Any());
        if (setupMethod is not null && !setupMethod.IsStatic)
        {
            throw new SetupFixtureException($"{nameof(SetupFixtureAttribute)} must be used on a static method");
        }

        if (setupMethod is not null && setupMethod.ReturnType != typeof(void))
        {
            throw new SetupFixtureException(
                $"{nameof(SetupFixtureAttribute)} must be used on a method which returns void");
        }

        return setupMethod;
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

    private class SetupFixtureCustomizationWrapper : ICustomization
    {
        private readonly MethodInfo _methodInfo;

        public SetupFixtureCustomizationWrapper(MethodInfo setupMethod)
        {
            _methodInfo = setupMethod;
        }

        public void Customize(IFixture fixture)
        {
            _methodInfo.Invoke(null, new object?[] { fixture });
        }
    }
}