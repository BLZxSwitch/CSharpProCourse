using FluentValidation;
using Tickets.Auth.DTO;

namespace Tickets.Auth.Validators;

internal class AuthRequestValidator : AbstractValidator<AuthRequest>
{
  public AuthRequestValidator()
  {
    RuleFor(request => request.Login).NotEmpty();
    RuleFor(request => request.Password).NotEmpty();
  }
}
