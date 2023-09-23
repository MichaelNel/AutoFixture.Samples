namespace AutoFixture.Attributes.Tests;

public class CustomAutoDomainDataAttributeTest
{
    [Theory]
    [CustomAutoDomainData(typeof(MyCustomization))]
    public void CustomAutoDomainData__AppliesCustomizations(int a, int b, int c)
    {
        a.ShouldBe(5);
        b.ShouldBe(5);
        c.ShouldBe(5);
    }
}

public class MyCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<int>(c => c.FromFactory(() => 5));
    }
}