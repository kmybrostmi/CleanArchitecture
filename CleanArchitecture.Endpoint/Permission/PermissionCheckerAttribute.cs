using CleanArchitecture.Application.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.Endpoint.Permission;
public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
{
    private IUserService _userService;
    private Guid _permissionId = Guid.Empty;
    public PermissionCheckerAttribute(string permissionId)
    {
        _permissionId = Guid.Parse(permissionId);
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var phoneNumber = context.HttpContext.User.Identity.Name;



            if (!_userService.CheckPermission(_permissionId, phoneNumber))
            {
                context.Result = new RedirectResult("/Login");
            }
        }
        else
        {
            context.Result = new RedirectResult("/Login");
        }
    }
}
