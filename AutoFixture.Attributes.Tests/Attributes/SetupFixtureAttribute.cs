using JetBrains.Annotations;

namespace AutoFixture.Attributes.Tests.Attributes;

[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method)]
[IgnoreXunitAnalyzersRule1013]
public class SetupFixtureAttribute : Attribute
{
}

// https://xunit.net/xunit.analyzers/rules/xUnit1013
public sealed class IgnoreXunitAnalyzersRule1013Attribute : Attribute
{
}