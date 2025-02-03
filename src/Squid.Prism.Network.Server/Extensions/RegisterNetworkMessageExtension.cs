using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Network.Data.Internal;
using Squid.Prism.Server.Core.Extensions;

namespace Squid.Prism.Network.Server.Extensions;

public static class RegisterNetworkMessageExtension
{
    public static IServiceCollection RegisterNetworkMessage(this IServiceCollection services, int messageType, Type message)
    {
        return services.AddToRegisterTypedList(new MessageTypeObject(messageType, message));
    }

    public static IServiceCollection RegisterNetworkMessage<T>(this IServiceCollection services, int messageType)
    {
        return services.RegisterNetworkMessage(messageType, typeof(T));
    }
}
