using AutoFixture.Attributes.Tests.Attributes;
using Moq;

namespace AutoFixture.Attributes.Tests;

public class AutoDomainDataAttributeMoqTest
{
    [SetupFixture]
    public static void Setup(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IMyInterface>>();
        mock.Setup(m => m.GetInt()).Returns(4);
    }

    [Theory]
    [AutoDomainData]
    public void SetupFixture__ReturnsInterface(IMyInterface myInterface)
    {
        var result = myInterface.GetInt();
        result.ShouldBe(4);
    }

    [Theory]
    [AutoDomainData]
    public void SetupFixture__ReturnsFrozenMock(Mock<IMyInterface> mockMyInterface, IMyInterface myInterface)
    {
        mockMyInterface.Setup(m => m.GetInt()).Returns(3);
        var result = myInterface.GetInt();
        result.ShouldBe(3);
    }
}

public interface IMyInterface
{
    public int GetInt();
}