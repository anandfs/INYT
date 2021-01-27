using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Controllers
{
    public class EmailFakeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
