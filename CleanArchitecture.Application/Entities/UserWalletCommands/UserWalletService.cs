using CleanArchitecture.Domain.Entities.Wallet;
using CleanArchitecture.Domain.ViewModels.Wallet;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using CleanArchitecture.Infrastructure.Repositories.Entities.UsersWallet;

namespace CleanArchitecture.Application.Entities.UserWalletCommands;

public class UserWalletService : IUserWalletService
{
    private readonly IUserWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;

    public UserWalletService(IUserWalletRepository walletRepository,IUserRepository userRepository)
	{
        _walletRepository = walletRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> ChargeWallet(Guid userId,Guid createById, ChargeWalletViewModel walletViewModel, string description)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user == null)
            return Guid.Empty;

        var chargeWallet = new UserWallet()
        {
            Amount = walletViewModel.Amount,
            CreateById = createById,
            IsActived = true,
            IsDeleted = false,
            IsPay = false,
            WalletType = WalletType.Variz,
            UserId = userId,
            Description = description
        };

        await _walletRepository.CreateWallet(chargeWallet);
        await _walletRepository.Save();
        return chargeWallet.Id;
    }

    public async Task<UserWallet> GetUserWalletById(Guid userId)
    {
        return await _walletRepository.GetUserWalletById(userId);
    }

    public async Task<bool> UpdateWalletForCharge(UserWallet userWallet)
    {
        if(userWallet != null)
        {
            userWallet.IsPay = true;
            _walletRepository.Update(userWallet);
            await _walletRepository.Save();
            return true;
        }
        return false;
    }
}