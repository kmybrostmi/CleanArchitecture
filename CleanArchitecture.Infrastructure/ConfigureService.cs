using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Entities.Products;
using CleanArchitecture.Infrastructure.Repositories.Entities.Roles;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using CleanArchitecture.Infrastructure.Repositories.Entities.UsersWaller;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;
public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserWalletRepository, UserWalletRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();

        return services;
    }
}
