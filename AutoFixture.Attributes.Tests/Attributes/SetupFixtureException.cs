using System.Runtime.Serialization;

namespace AutoFixture.Attributes.Tests.Attributes;

public class SetupFixtureException : Exception
{
    public SetupFixtureException()
    {
    }

    protected SetupFixtureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public SetupFixtureException(string? message) : base(message)
    {
    }

    public SetupFixtureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}