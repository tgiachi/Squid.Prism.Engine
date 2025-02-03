namespace Squid.Prism.Engine.Core.Configs;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigVariableAttribute : Attribute
{
    public string? Name { get; set; }

    public ConfigVariableAttribute(string? name = null)
    {
        Name = name;
    }
}
