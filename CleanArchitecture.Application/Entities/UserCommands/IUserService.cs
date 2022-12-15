using CleanArchitecture.Domain.Entities.Account;

namespace CleanArchitecture.Application.Entities.UserCommands;

public interface IUserService
{
    Task<Users> GetUserByPhoneNumber(string phoneNumber);
}
