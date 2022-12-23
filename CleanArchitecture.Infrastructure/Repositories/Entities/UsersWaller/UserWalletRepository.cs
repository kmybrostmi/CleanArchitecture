using CleanArchitecture.Domain.Entities.Wallet;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.UsersWallet;

public class UserWalletRepository : BaseRepository<UserWallet>, IUserWalletRepository
{
    public UserWalletRepository(AppDbContext context) : base(context)
    {
    }

    public async Task CreateWallet(UserWallet wallet)
    {
        await Context.UserWallets.AddAsync(wallet);
    }

    public async Task<UserWallet> GetUserWalletById(Guid userId)
    {
        return await Context.UserWallets.SingleOrDefaultAsync(x => x.UserId == userId);
    }
}
