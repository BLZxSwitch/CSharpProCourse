using FluentValidation;
using Tickets.Auth.Controllers;
using Tickets.Auth.DTO;

namespace Tickets.Auth.Validators;

/// <summary>
/// Validator for RegisterRequest
/// </summary>
internal sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(request => request.Password).NotEmpty();
    RuleFor(request => request.Email).EmailAddress();
  }
}
