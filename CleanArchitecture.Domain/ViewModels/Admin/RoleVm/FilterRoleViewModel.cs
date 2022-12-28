using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Account;

namespace CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
public class FilterRoleViewModel : BasePaging
{
    public string SearchTerm { get; set; }
    public List<Role> Roles { get; set; }

    public FilterRoleViewModel SetRoles(List<Role> roles)
    {
        this.Roles = roles;
        return this;
    }

    public FilterRoleViewModel SetPaging(BasePaging paging)
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
