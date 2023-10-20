using AutoFixture.Attributes.Tests.Attributes;
using AutoFixture.Attributes.Tests.Customizations;

namespace AutoFixture.Attributes.Tests;

public class InlineAutoDomainDataAttributeTest
{
    [SetupFixture]
    public static void SetupFixture(IFixture fixture)
    {
        fixture.Customize(new MyLongCustomization());
    }

    [Theory]
    [InlineAutoDomainData("foo")]
    public void InlineTest(string inlineString, long myLong)
    {
        inlineString.ShouldBe("foo");
        myLong.ShouldBe(200);
    }
}