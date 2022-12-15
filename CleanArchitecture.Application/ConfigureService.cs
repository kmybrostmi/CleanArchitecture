using CleanArchitecture.Application.Entities.UserCommands;
using CleanArchitecture.Application.Entities.UserCommands.Create;
using CleanArchitecture.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPasswordHelper, PasswordHelper>();
        services.AddMediatR(typeof(RegisterUserCommand).Assembly);

        return services;
    }
}

