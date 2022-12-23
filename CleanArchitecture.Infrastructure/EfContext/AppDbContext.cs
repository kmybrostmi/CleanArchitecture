using CleanArchitecture.Domain.Entities.Account;
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
}

