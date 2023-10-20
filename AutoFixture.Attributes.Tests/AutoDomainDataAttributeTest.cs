using AutoFixture.Attributes.Tests.Attributes;

namespace AutoFixture.Attributes.Tests;

public class AutoDomainDataAttributeSetupFixtureTest
{
    [SetupFixture]
    public static void SetupFixture(IFixture fixture)
    {
        fixture.Customize<int>(c => c.FromFactory(() => 2));
    }

    [Theory]
    [AutoDomainData]
    public void Attribute_WithSetupMethod_AppliesCustomizations(int a, int b, int c)
    {
        a.ShouldBe(2);
        b.ShouldBe(2);
        c.ShouldBe(2);
    }
}

public class AutoDomainDataAttributeTest
{
    [Theory]
    [AutoDomainData]
    public void SetupFixture_WithoutSetupMethod_AppliesFixtureFactoryCustomizations(string a)
    {
        a.ShouldBe("bananas");
    }
}