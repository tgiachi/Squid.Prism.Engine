namespace Squid.Prism.Server.Data.Runtime;

public class RuntimeData
{
    public bool IsDocker { get; set; }

    public string RootDirectory { get; set; }

    public int ProcessCount { get; set; }

    public int ProcessId { get; set; }


    public override string ToString()
    {
        return $"IsDocker: {IsDocker}, RootDirectory: {RootDirectory}, ProcessCount: {ProcessCount}, ProcessId: {ProcessId}";
    }
}
