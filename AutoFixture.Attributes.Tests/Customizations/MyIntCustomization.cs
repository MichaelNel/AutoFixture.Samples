namespace AutoFixture.Attributes.Tests.Customizations;

public class MyIntCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<int>(c => c.FromFactory(() => 100));
    }
}