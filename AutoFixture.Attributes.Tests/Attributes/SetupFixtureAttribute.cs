using JetBrains.Annotations;

namespace AutoFixture.Attributes.Tests.Attributes;

[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method)]
public class SetupFixtureAttribute : Attribute
{
}