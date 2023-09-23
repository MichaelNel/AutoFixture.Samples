namespace AutoFixture.Attributes.Tests.Customizations;

public class MyLongCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<long>(c => c.FromFactory(() => 200));
    }
}