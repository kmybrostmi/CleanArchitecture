using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Wallet;
using CleanArchitecture.Domain.ViewModels.Wallet;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.UsersWaller;

public class UserWalletRepository : BaseRepository<UserWallet>, IUserWalletRepository
{
    public UserWalletRepository(AppDbContext context) : base(context)
    {
    }

    public async Task CreateWallet(UserWallet wallet)
    {
        await Context.UserWallets.AddAsync(wallet);
    }

    public async Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter)
    {
        var query = Context.UserWallets.AsQueryable();

        if (filter.UserId != Guid.Empty && filter.UserId != null)
        {
            query = query.Where(c => c.UserId == filter.UserId);
        }

        var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return filter.SetPaging(pager).SetWallets(allData);
    }

    public async Task<UserWallet> GetUserWalletById(Guid userId)
    {
        return await Context.UserWallets.SingleOrDefaultAsync(x => x.UserId == userId);
    }
}

