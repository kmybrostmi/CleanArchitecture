using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.Entities.Products;
using CleanArchitecture.Domain.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.EfContext;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<Users> Users { get; set; }
    public DbSet<UserWallet> UserWallets { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    public DbSet<Product> Products { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductGalleries> ProductGalleries { get; set; }
    public DbSet<ProductCategory> ProductsCategories { get; set; }
}







