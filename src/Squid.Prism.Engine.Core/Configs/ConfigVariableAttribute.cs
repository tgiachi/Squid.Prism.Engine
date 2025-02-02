namespace Squid.Prism.Engine.Core.Configs;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigVariableAttribute : Attribute
{
    public string? Name { get; set; }
}
