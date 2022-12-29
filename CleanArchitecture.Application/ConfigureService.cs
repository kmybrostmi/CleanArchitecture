using CleanArchitecture.Application.Entities.Products;
using CleanArchitecture.Application.Entities.Roles;
using CleanArchitecture.Application.Entities.User;
using CleanArchitecture.Application.Entities.User.Create;
using CleanArchitecture.Application.Entities.UserWallets;
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
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IPasswordHelper, PasswordHelper>();
        services.AddMediatR(typeof(RegisterUserCommand).Assembly);
        services.AddScoped<ISmsService, SmsService>();
        return services;
    }
}

