using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Users> GetUserByPhoneNumber(string phoneNumber)
    {
        var result =  await Context.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        return result;
    }

    public async Task<bool> IsExistsPhoneNumber(string phoneNumber)
    {
        return await Context.Users.AsQueryable().AnyAsync(x => x.PhoneNumber == phoneNumber);
    }
}


