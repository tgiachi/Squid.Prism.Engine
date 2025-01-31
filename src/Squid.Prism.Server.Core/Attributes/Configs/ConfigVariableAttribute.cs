namespace Squid.Prism.Server.Core.Attributes.Configs;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigVariableAttribute : Attribute
{
    public string Name { get; set; }
}
