using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.RoleVm;

namespace CleanArchitecture.Application.Entities.RolesCommands;
public interface IRoleService
{
    Task<FilterRoleViewModel> FilterRole(FilterRoleViewModel filterRoleViewModel);

    Task<CreateOrEditRoleResult> CreateOrEditRoleForAdmin(CreateOrEditRoleViewModel viewModel);
    Task<CreateOrEditRoleViewModel> GetRoleById(Guid roleId);
    Task<List<Permission>> GetAllActiveRolePermission();
    Task<List<Role>> GetAllActiveRoles();
}





