using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Wallet;

namespace CleanArchitecture.Domain.ViewModels.Wallet;

public class FilterWalletViewModel : BasePaging
{
    public Guid? UserId { get; set; }
    public List<UserWallet> UserWallets { get; set; }

    public FilterWalletViewModel SetWallets(List<UserWallet> userWallets)
    {
        this.UserWallets = userWallets;
        return this;
    }

    public FilterWalletViewModel SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllEntityCount = paging.AllEntityCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.TakeEntity = paging.TakeEntity;
        this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
        this.SkipEntitiy = paging.SkipEntitiy;
        this.PageCount = paging.PageCount;

        return this;
    }
}