using CleanArchitecture.Domain.Common.Paging;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
using CleanArchitecture.Infrastructure.EfContext;
using CleanArchitecture.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Roles;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<FilterRoleViewModel> FilterRole(FilterRoleViewModel viewModel)
    {
        var query = Context.Roles.AsQueryable().Where(x => x.IsActived && !x.IsDeleted);

        if(!string.IsNullOrWhiteSpace(viewModel.SearchTerm))
        {
            query = query.Where(x => x.RoleTitle.Contains(viewModel.SearchTerm));
        }

        var pager = Pager.Build(viewModel.PageId, await query.CountAsync(), viewModel.TakeEntity, viewModel.CountForShowAfterAndBefor);
        var allData = await query.Paging(pager).ToListAsync();

        return viewModel.SetPaging(pager).SetRoles(allData);
    }


    public async Task AddRolePermission(List<Guid> selctedPermission, Guid roleId)
    {
        if (selctedPermission != null && selctedPermission.Any())
        {
            var rolePermissions = new List<RolePermission>();

            foreach (var permissionId in selctedPermission)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionId = permissionId,
                    RoleId = roleId,
                });
            }
            await Context.RolePermissions.AddRangeAsync(rolePermissions);
            await Context.SaveChangesAsync();
        }
    }

    public async Task RemoveAllRolePermission(Guid roleId)
    {
        var rolePermission = await Context.RolePermissions.Where(x => x.RoleId == roleId).ToListAsync();
        
        if(rolePermission.Any())
        {
            Context.RolePermissions.RemoveRange(rolePermission);    
        }
    }

    public Task<Role> GetRoleById(Guid roleId)
    {
        return Context.Roles.Include(x=>x.RolePermissions).FirstOrDefaultAsync(x => x.Id == roleId);
    }

    public async Task<List<Permission>> GetAllActiveRolePermission()
    {
        var result = await Context.Permissions.Where(x=>x.IsActived && !x.IsDeleted).ToListAsync();
        return result;  
    }
}

