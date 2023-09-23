namespace AutoFixture.Attributes.Tests;

public class FlexibleAutoDomainDataAttributeTest
{
    [SetupFixture]
    public static IFixture SetupFixture(IFixture fixture)
    {
        fixture.Customize<int>(c => c.FromFactory(() => 5));
        fixture.Customize<double>(c => c.FromFactory(() => 1.2));
        return fixture;
    }

    [Theory]
    [FlexibleAutoDomainData(typeof(MyStringCustomization))]
    public void FlexibleAutoDomainDataAttribute_WithMethodCustomizations_OverridesFixtureSetup(int myInt,
        double myDouble, string myString)
    {
        myInt.ShouldBe(8);
        myDouble.ShouldBe(1.2);
        myString.ShouldBe("Bananas");
    }

    private class MyStringCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<int>(c => c.FromFactory(() => 8));
            fixture.Customize<string>(c => c.FromFactory(() => "Bananas"));
        }
    }
}