using Microsoft.AspNetCore.Identity;

namespace Tickets.Auth.DTO;

public class RegisterResponse
{
  public bool Success { get; init; }
  public IEnumerable<IdentityError>? Errors { get; init; } = default!;
}
