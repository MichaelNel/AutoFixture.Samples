namespace AutoFixture.Attributes.Tests;

public class AutoDomainDataAttributeTest
{
    [SetupFixture]
    public static IFixture SetupFixture(IFixture fixture)
    {
        fixture.Customize<int>(c => c.FromFactory(() => 2));
        return fixture;
    }

    [Theory]
    [AutoDomainData]
    public void SetupFixture__AppliesCustomizations(int a, int b, int c)
    {
        a.ShouldBe(2);
        b.ShouldBe(2);
        c.ShouldBe(2);
    }
}