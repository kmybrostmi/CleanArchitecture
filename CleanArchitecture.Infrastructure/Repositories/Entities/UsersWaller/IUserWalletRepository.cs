using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.Entities.Wallet;
using CleanArchitecture.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.UsersWallet;
public interface IUserWalletRepository : IBaseRepository<UserWallet>
{
    Task CreateWallet(UserWallet wallet);
    Task<UserWallet> GetUserWalletById(Guid userId);
}
