using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Auth;
using Tickets.Auth.Validators;
using Tickets.Core.Configurations;
using Tickets.Core.UserAggregate;
using Tickets.Infrastructure;
using Tickets.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddIdentityCore<User>();

builder.Services.AddIdentity<User, Role>(cfg =>
  {
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredLength = 6;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
  })
  .AddEntityFrameworkStores<AppDbContext>()
  .AddUserManager<UserManager<User>>()
  .AddRoleManager<RoleManager<Role>>();
// .AddSignInManager<SignInManager<User>>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// builder.Services.AddDbContext(connectionString!);
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString)
    .UseSnakeCaseNamingConvention()
    .UseLazyLoadingProxies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger("Tickets Auth Web API V1");

builder.Services.AddValidatorsFromAssemblyContaining<AuthRequestValidator>(includeInternalTypes: true);
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  // containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultAuthModule());
  containerBuilder.RegisterModule(
    new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

// if (authTokenSettings != null)
// {
builder.ConfigureBearerAuth();
// }

// builder.Services.AddScoped<ITokenService, TokenService>();
// builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
