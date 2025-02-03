using System.ComponentModel.DataAnnotations.Schema;
using Squid.Prism.Server.Core.Interfaces.Entities;

namespace Squid.Prism.Server.Core.Entities;

[Table("users")]
public class UserEntity : IBaseDbEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
