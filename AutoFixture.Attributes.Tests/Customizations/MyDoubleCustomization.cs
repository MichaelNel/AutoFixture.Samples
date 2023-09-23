namespace AutoFixture.Attributes.Tests.Customizations;

public class MyDoubleCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<double>(c => c.FromFactory(() => 1.1));
    }
}