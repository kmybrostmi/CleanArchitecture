using CleanArchitecture.Application.Entities.RolesCommands;
using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Entities.UserCommands.Create;
using CleanArchitecture.Application.Entities.UserWalletCommands;
using CleanArchitecture.Application.Helpers.Interfaces;
using CleanArchitecture.Application.Helpers.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserWalletService, UserWalletService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IPasswordHelper, PasswordHelper>();
        services.AddMediatR(typeof(RegisterUserCommand).Assembly);
        services.AddScoped<ISmsService, SmsService>();
        return services;
    }
}

