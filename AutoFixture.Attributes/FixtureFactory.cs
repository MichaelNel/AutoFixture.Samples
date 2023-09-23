using AutoFixture.AutoMoq;

namespace AutoFixture.Attributes;

public static class FixtureFactory
{
    public static IFixture Create()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        return fixture;
    }
}