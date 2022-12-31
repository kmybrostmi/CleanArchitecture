using CleanArchitecture.Endpoint.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Areas.Admin.Controllers;

[Authorize]
[Area("Admin")]
[PermissionChecker("E6CD6056-0EB4-4D67-B5AD-8D2E69C83637")]
public class AdminBaseController : Controller
{
    protected string ErrorMessage = "ErrorMessage";
    protected string SuccessMessage = "SuccessMessage";
    protected string WarningMessage = "WarningMessage";
    protected string InfoMessage = "InfoMessage";
}
