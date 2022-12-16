using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Endpoint.Controllers
{
    public class BaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string InfoMessage = "InfoMessage";
    }
}

