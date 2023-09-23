using System.Reflection;
using AutoFixture.Attributes.Tests.Customizations;
using AutoFixture.Xunit2;

namespace AutoFixture.Attributes.Tests.Attributes;

public class DoubleCustomizeAttribute : CustomizeAttribute
{
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        return new MyDoubleCustomization();
    }
}