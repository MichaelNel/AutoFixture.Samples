namespace AutoFixture.Attributes.Tests;

public class AutoDomainDataAttribute : SetupAutoDataAttribute
{
    public AutoDomainDataAttribute() : base(FixtureFactory.Create)
    {
    }
}