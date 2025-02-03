using Microsoft.Extensions.DependencyInjection;
using Squid.Prism.Server.Core.Data.Services;

namespace Squid.Prism.Server.Core.Extensions;

public static class RegisterDatabaseEntityExtension
{
    public static IServiceCollection RegisterDatabaseEntity(
        this IServiceCollection services, Type entityType
    )
    {
        services.AddToRegisterTypedList(new DbEntityTypeData(entityType));

        return services;
    }

    public static IServiceCollection RegisterDatabaseEntity<T>(this IServiceCollection services)
    {
        return services.RegisterDatabaseEntity(typeof(T));
    }
}
