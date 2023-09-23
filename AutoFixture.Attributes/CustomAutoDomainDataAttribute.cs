using AutoFixture.Xunit2;

namespace AutoFixture.Attributes;

public class CustomAutoDomainDataAttribute : AutoDataAttribute
{
    public CustomAutoDomainDataAttribute(params Type[] customizationTypes)
        : base(() => CreateFixture(customizationTypes))
    {
    }

    private static IFixture CreateFixture(IEnumerable<Type> customizationTypes)
    {
        var fixture = FixtureFactory.Create();
        foreach (var customizationType in customizationTypes)
        {
            var customization = (ICustomization)Activator.CreateInstance(customizationType)!;
            fixture.Customize(customization);
        }

        return fixture;
    }
}