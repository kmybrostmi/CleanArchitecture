using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.UserVm;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.User;
public interface IUserRepository : IBaseRepository<Users>
{
    Task<bool> DeleteUser(Guid userId);
    Task<bool> IsExistsPhoneNumber(string phoneNumber);
    Task<Users> GetUserByPhoneNumber(string phoneNumber);
    void UpdateUser(Users user);
    Task<Users> GetUserById(Guid id);
    Task<Users> GetUserAndRolesById(Guid id);

    //Admin
    Task<FilterUserViewModel> FilterUser(FilterUserViewModel filterUser);
}







