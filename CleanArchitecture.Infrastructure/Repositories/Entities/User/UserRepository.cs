using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.UserVm;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.User;

public class UserRepository : BaseRepository<Users>, IUserRepository
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
        var result = await Context.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        return result;
    }

    public async Task<bool> IsExistsPhoneNumber(string phoneNumber)
    {
        return await Context.Users.AsQueryable().AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public void UpdateUser(Users user)
    {
        Context.Users.Update(user);
    }

    public async Task<Users> GetUserById(Guid id)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<FilterUserViewModel> FilterUser(FilterUserViewModel filterUser)
    {
        var query = Context.Users.AsQueryable();

        if(!string.IsNullOrWhiteSpace(filterUser.SearchTearm))
        {
            query = query.Where(x=>x.PhoneNumber.Contains(filterUser.SearchTearm) ||
                                                      x.FirstName.Contains(filterUser.SearchTearm) ||
                                                      x.LastName.Contains(filterUser.SearchTearm));
        }

        var pager = Pager.Build(filterUser.PageId, await query.CountAsync(), filterUser.TakeEntity, filterUser.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return filterUser.SetPaging(pager).SetUsers(allData);
    }
}



