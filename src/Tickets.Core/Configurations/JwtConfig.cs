using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Tickets.Core.Options;

namespace Tickets.Core.Configurations;

public static class JwtConfig
{
  public static void ConfigureBearerAuth(this WebApplicationBuilder builder)
  {
    var authTokenSettingsSection = builder.Configuration.GetSection(nameof(AuthTokenOptions));
    var options = authTokenSettingsSection.Get<AuthTokenOptions>();
    builder.Services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(opt =>
      {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = options?.Issuer,
          ValidAudience = options?.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options?.SecretKey ?? ""))
        };
      });
    
    builder.Services.Configure<AuthTokenOptions>(authTokenSettingsSection);
  }
}
