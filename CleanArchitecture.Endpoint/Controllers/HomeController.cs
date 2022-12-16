using CleanArchitecture.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanArchitecture.Endpoint.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //TempData[ErrorMessage] = "123";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}