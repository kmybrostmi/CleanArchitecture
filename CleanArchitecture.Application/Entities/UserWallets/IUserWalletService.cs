using CleanArchitecture.Domain.Entities.Wallet;
using CleanArchitecture.Domain.ViewModels.Wallet;

namespace CleanArchitecture.Application.Entities.UserWallets;
public interface IUserWalletService
{
    Task<Guid> ChargeWallet(Guid userId, Guid createById, ChargeWalletViewModel walletViewModel, string description);
    Task<UserWallet> GetUserWalletById(Guid userId);
    Task<bool> UpdateWalletForCharge(UserWallet userWallet);

    Task<FilterWalletViewModel> FilterWallets(FilterWalletViewModel filter);
}
