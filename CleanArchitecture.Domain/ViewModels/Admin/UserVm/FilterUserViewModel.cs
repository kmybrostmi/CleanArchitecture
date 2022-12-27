using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Account;

namespace CleanArchitecture.Domain.ViewModels.Admin.UserVm;
public class FilterUserViewModel : BasePaging
{
    public string SearchTearm { get; set; }
    public List<Users> Users { get; set; }

    public FilterUserViewModel SetUsers(List<Users> users)
    {
        this.Users = users;
        return this;
    }

    public FilterUserViewModel SetPaging(BasePaging paging)
    {
        this.PageId = paging.PageId;
        this.AllEntityCount = paging.AllEntityCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.TakeEntity = paging.TakeEntity;
        this.CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
        this.SkipEntitiy = paging.SkipEntitiy;
        this.PageCount = paging.PageCount;

        return this;
    }
}

