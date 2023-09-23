namespace AutoFixture.Attributes.Tests;

public class BasicUsageTest
{
    private readonly IFixture _fixture = FixtureFactory.Create();

    public BasicUsageTest()
    {
        _fixture.Customize<int>(c => c.FromFactory(() => 9));
    }

    [Fact]
    public void Create__UsesSetup()
    {
        _fixture.Create<int>().ShouldBe(9);
        _fixture.Create<int>().ShouldBe(9);
        _fixture.Create<int>().ShouldBe(9);
    }
}