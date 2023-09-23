using AutoFixture.Xunit2;

namespace AutoFixture.Attributes.Tests.Attributes;

public class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoDomainDataAttribute(params object[] values) : base(new AutoDomainDataAttribute(), values)
    {
    }
}