using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INYTWebsite.Models;
using INYTWebsite.Model;

namespace INYTWebsite.Controllers
{
    public class HomeController : Controller
    {

        INYTContext db = new INYTContext();

        public IActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult Search()
        {
            return View("SearchResults", db.Tradesperson.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
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
