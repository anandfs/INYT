using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace INYTWebsite.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    public class CustomerProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Calendar")]
        public IActionResult Calendar()
        {
            return View();
        }

        public IActionResult Bookings()
        {
            return View();
        }

        public IActionResult Invoices()
        {
            return View();
        }
    }
}