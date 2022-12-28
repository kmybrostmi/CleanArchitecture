using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
using CleanArchitecture.Infrastructure.Repositories.Common;

namespace CleanArchitecture.Infrastructure.Repositories.Entities.Roles;
public interface IRoleRepository : IBaseRepository<Role>
{
    Task<FilterRoleViewModel> FilterRole(FilterRoleViewModel viewModel);

    Task AddRolePermission(List<Guid> selctedPermission, Guid roleId);
    Task RemoveAllRolePermission(Guid roleId);

    Task<Role> GetRoleById(Guid roleId);

    Task<List<Permission>> GetAllActiveRolePermission();
}


