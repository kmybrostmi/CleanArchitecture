using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.User;
public interface IUserRepository : IBaseRepository<Users>
{
    Task<bool> DeleteUser(Guid userId);
    Task<bool> IsExistsPhoneNumber(string phoneNumber);
    Task<Users> GetUserByPhoneNumber(string phoneNumber);
}






