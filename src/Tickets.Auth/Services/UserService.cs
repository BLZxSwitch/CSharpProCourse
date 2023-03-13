using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tickets.Auth.DTO;
using Tickets.Auth.Interfaces;
using Tickets.Core.UserAggregate;
using Tickets.Core.UserAggregate.Enums;

namespace Tickets.Auth.Services;

internal sealed class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;
  private readonly RoleManager<Role> _roleManager;

  public UserService(
    UserManager<User> userManager,
    ITokenService tokenService,
    RoleManager<Role> roleManager
  )
  {
    _userManager = userManager;
    _tokenService = tokenService;
    _roleManager = roleManager;
  }

  public Task<bool> CheckPasswordAsync(User user, string password)
  {
    return _userManager.CheckPasswordAsync(user, password);
  }

  public Task<User?> FindByNameAsync(string userName)
  {
    return _userManager.FindByNameAsync(userName);
  }

  public async Task<string?> GetAuthTokenOnUserLogin(AuthRequest request)
  {
    var user = await FindByNameAsync(request.Login);

    if (user == null)
    {
      return null;
    }

    var valid = await CheckPasswordAsync(user, request.Password);

    return valid ? _tokenService.GetAuthToken(user) : null;
  }

  public async Task<RegisterResponse> RegisterUser(RegisterRequest request)
  {
    var clientRole = await _roleManager.Roles.FirstAsync(role => role.Name == Roles.Client.ToString());
    var userId = Guid.NewGuid();
    var user = new User
    {
      Id = userId,
      IsActive = true,
      Email = request.Email,
      UserName = request.Email,
      Roles = new List<UserRole> { new() { RoleId = clientRole.Id, UserId = userId, } }
    };

    var result = await _userManager.CreateAsync(user, request.Password);

    return new RegisterResponse { Success = result.Succeeded, Errors = result.Errors.Any() ? result.Errors : null };
  }

  public async Task AddAdminRole(User user, Roles roles)
  {
    await _userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
  }
}
