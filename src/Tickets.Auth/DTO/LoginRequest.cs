namespace Tickets.Auth.DTO;

public class AuthRequest
{
  public string Login { get; init; } = String.Empty;
  public string Password { get; init; } = String.Empty;
}
