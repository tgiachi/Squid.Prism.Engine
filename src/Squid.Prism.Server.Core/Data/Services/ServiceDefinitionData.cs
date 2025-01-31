namespace Squid.Prism.Server.Core.Data.Services;

public struct ServiceDefinitionData(Type serviceType, Type implementationType, int priority)
{
    public Type ServiceType { get; set; } = serviceType;
    public Type ImplementationType { get; set; } = implementationType;

    public int Priority { get; set; } = priority;
}
