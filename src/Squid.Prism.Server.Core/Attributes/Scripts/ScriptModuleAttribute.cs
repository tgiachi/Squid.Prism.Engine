namespace Squid.Prism.Server.Core.Attributes.Scripts;

[AttributeUsage(AttributeTargets.Class)]
public class ScriptModuleAttribute : Attribute
{
    public string TableName { get; set; }

    public ScriptModuleAttribute(string tableName)
    {
        TableName = tableName;
    }
}
