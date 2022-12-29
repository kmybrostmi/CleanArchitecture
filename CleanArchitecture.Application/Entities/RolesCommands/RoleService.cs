using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
using CleanArchitecture.Infrastructure.Repositories.Entities.Roles;

namespace CleanArchitecture.Application.Entities.RolesCommands;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateOrEditRoleResult> CreateOrEditRoleForAdmin(CreateOrEditRoleViewModel viewModel)
    {
        if (viewModel.Id == Guid.Empty)
        {
            var newRole = new Role()
            {
                RoleTitle = viewModel.RoleTitle,
                CreateById = viewModel.CreateBy,
                CreateDate = DateTime.Now
            };

            //if (viewModel.SelectedPermissions == null)
            //{
            //    return CreateOrEditRoleResult.NotExistPermissions;
            //}

            //await _repository.AddRolePermission(viewModel.SelectedPermissions, newRole.Id);
            _repository.Add(newRole);
            await _repository.Save();
            return CreateOrEditRoleResult.Success;
        }

        var role = await _repository.GetRoleById(viewModel.Id);

        if (role == null)
            return CreateOrEditRoleResult.NotFound;

        role.ModifiedById = viewModel.ModifiedBy;
        role.RoleTitle = viewModel.RoleTitle;
        role.ModifiedDate = DateTime.Now;

        _repository.Update(role);

        if (viewModel.SelectedPermissions == null)
            return CreateOrEditRoleResult.NotExistPermissions;
        

        await _repository.RemoveAllRolePermission(role.Id);

        await _repository.AddRolePermission(viewModel.SelectedPermissions,role.Id);

        await _repository.Save();

        return CreateOrEditRoleResult.Success;
    }

    public async Task<FilterRoleViewModel> FilterRole(FilterRoleViewModel filterRoleViewModel)
    {
        var result = await _repository.FilterRole(filterRoleViewModel);
        return result;
    }

    public async Task<List<Permission>> GetAllActiveRolePermission()
    {
        var result = await _repository.GetAllActiveRolePermission();
        return result;
    }

    public async Task<List<Role>> GetAllActiveRoles()
    {
        var result = await _repository.GetAllActiveRoles();
        return result;
    }

    public async Task<CreateOrEditRoleViewModel> GetRoleById(Guid roleId)
    {
        var role = await _repository.GetRoleById(roleId);

        return new CreateOrEditRoleViewModel
        {
            Id = roleId,
            RoleTitle = role.RoleTitle,
            SelectedPermissions = role.RolePermissions.Select(x => x.Id).ToList()
        };
    }
}
