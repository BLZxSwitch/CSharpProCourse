using Microsoft.AspNetCore.Identity;
using Tickets.Core.UserAggregate.Extensions;

namespace Tickets.Core.UserAggregate;

public class User : IdentityUser<Guid>
{
  public bool IsActive { get; init; }
  public virtual ICollection<UserRole> Roles { get; set; } = default!;
  public bool IsAdministrator => UserExpressions.IsAdministrator.Compile()(this);
  public bool IsManager => UserExpressions.IsManager.Compile()(this);
  public bool IsClient => UserExpressions.IsClient.Compile()(this);
}
