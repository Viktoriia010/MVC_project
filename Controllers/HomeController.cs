using Microsoft.AspNetCore.Mvc;
using MvcProject.Models;
using System.Diagnostics;

namespace MvcProject.Controllers
{
    public class HomeController : Controller
    {
        public string Test(int a, int b, int c)
        {
            double d = b * b - 4 * a * c;
            if (a == 0)
            {
                return "<h2>a не може дорівнювати 0</h2>";
            }

            if (d < 0)
            {
                return "<h2>Коренів немає</h2>";
            }

            if (d == 0)
            {
                double x = -b / (2.0 * a);
                return $"Один корінь: x = {x}";
            }

            double x1 = (-b + Math.Sqrt(d)) / (2.0 * a);
            double x2 = (-b - Math.Sqrt(d)) / (2.0 * a);

            return $"x1 = {x1}, x2 = {x2}";
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
