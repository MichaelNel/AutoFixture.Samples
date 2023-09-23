namespace AutoFixture.Attributes.Tests.Customizations;

public class MyStringCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<string>(s => s.FromFactory(() => "bananas"));
    }
}