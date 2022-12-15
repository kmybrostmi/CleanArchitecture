using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.User;

public class UserRepository : BaseRepository<Users> , IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public Task<bool> DeleteUser(Guid userId)
    {
        throw new NotImplementedException();
    }
}


