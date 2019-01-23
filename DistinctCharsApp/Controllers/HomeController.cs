using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DistinctCharsApp.Models;

namespace DistinctCharsApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = Resource.AboutString;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Danny Mitchell.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
