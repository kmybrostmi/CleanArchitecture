using CleanArchitecture.Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanArchitecture.Endpoint.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}