using FastEndpoints.Swagger.Swashbuckle;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tickets.Core.Configurations;

public static class SwaggerConfig
{
  public static void UseAppSwagger(this WebApplication app, string name)
  {
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", name));
  }

  public static void ConfigureSwagger(this IServiceCollection services, string title)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });
      c.EnableAnnotations();
      c.OperationFilter<FastEndpointsOperationFilter>();
      var jwtSecurityScheme = new OpenApiSecurityScheme
      {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Введите токен",
        Reference = new OpenApiReference
        {
          Id = JwtBearerDefaults.AuthenticationScheme, Type = ReferenceType.SecurityScheme
        }
      };

      c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

      c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
    });
  }
}
