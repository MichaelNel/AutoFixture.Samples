using AutoFixture.Attributes.Tests.Attributes;
using AutoFixture.Attributes.Tests.Customizations;

namespace AutoFixture.Attributes.Tests;

public class CompleteCustomizationsTest
{
    [SetupFixture]
    public static IFixture SetupFixture(IFixture fixture)
    {
        return fixture.Customize(new MyLongCustomization());
    }

    /// <summary>
    ///     1. Double customization from custom parameter attribute.
    ///     2. Int customization from custom SetupAutoDataAttribute.
    ///     3. Long customization from SetupFixture marked method.
    ///     4. String customization from static fixture factory.
    /// </summary>
    [Theory]
    [IntCustomizedAutoDomainData]
    public void Test([DoubleCustomize] double myDouble, int myInt, long myLong, string myString)
    {
        myDouble.ShouldBe(1.1);
        myInt.ShouldBe(100);
        myLong.ShouldBe(200);
        myString.ShouldBe("bananas");
    }
}