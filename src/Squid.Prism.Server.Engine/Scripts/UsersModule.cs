using Microsoft.Extensions.Logging;
using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Entities;
using Squid.Prism.Server.Core.Interfaces.Services;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("users")]
public class UsersModule
{
    private readonly IDatabaseService _databaseService;

    private readonly ILogger _logger;

    public UsersModule(IDatabaseService databaseService, ILogger<UsersModule> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }


    [ScriptFunction("add_user", "Add a user")]
    public bool AddUser(string username, string password, bool isAdmin = false)
    {
        var existingUser = _databaseService.QueryAsync<UserEntity>(entity => entity.Username == username)
            .Result.FirstOrDefault();

        if (existingUser != null)
        {
            return false;
        }


        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = password,
            IsAdmin = isAdmin,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _databaseService.InsertAsync(user).Wait();

        _logger.LogInformation($"User {username} added is Admin: {isAdmin}");
        return true;
    }
}
