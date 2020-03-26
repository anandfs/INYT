using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace INYTWebsite.Areas.ServiceProvider.Controllers
{
    [Area("ServiceProvider")]
    public class ServiceProviderController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ServiceProviderModel model = new ServiceProviderModel();

            return View(model);
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}