using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;

namespace CleanArchitecture.Application.Entities.UserCommands;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<Users> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _repository.GetUserByPhoneNumber(phoneNumber);
    }
}
