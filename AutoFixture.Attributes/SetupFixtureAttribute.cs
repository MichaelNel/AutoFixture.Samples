using JetBrains.Annotations;

namespace AutoFixture.Attributes;

[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method)]
public class SetupFixtureAttribute : Attribute
{
}