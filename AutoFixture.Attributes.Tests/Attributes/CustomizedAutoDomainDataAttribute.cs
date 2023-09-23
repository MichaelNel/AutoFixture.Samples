using AutoFixture.Attributes.Tests.Customizations;

namespace AutoFixture.Attributes.Tests.Attributes;

public class IntCustomizedAutoDomainDataAttribute : SetupAutoDataAttribute
{
    public IntCustomizedAutoDomainDataAttribute() : base(() => FixtureFactory.Create()
        .Customize(new MyIntCustomization()))
    {
    }
}