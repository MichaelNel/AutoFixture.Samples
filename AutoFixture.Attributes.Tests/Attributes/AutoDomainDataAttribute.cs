namespace AutoFixture.Attributes.Tests.Attributes;

public class AutoDomainDataAttribute : SetupAutoDataAttribute
{
    public AutoDomainDataAttribute() : base(FixtureFactory.Create)
    {
    }
}