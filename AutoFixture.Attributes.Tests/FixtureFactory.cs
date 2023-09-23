using AutoFixture.Attributes.Tests.Customizations;
using AutoFixture.AutoMoq;

namespace AutoFixture.Attributes.Tests;

public static class FixtureFactory
{
    public static IFixture Create()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        fixture.Customize(new MyStringCustomization());
        return fixture;
    }
}