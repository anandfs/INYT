using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INYTWebsite.Models;
using INYTWebsite.Model;
using INYTWebsite.Code;

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
            string postcode = string.Empty;
            postcode = "W13 9XW";
            List<Serviceprovider> serviceproviders = new List<Serviceprovider>();

            foreach(var tradesperson in db.Tradesperson.ToList())
            {
                Serviceprovider sp = new Serviceprovider();
                sp.serviceprovidername = String.Format("{0} {1}", tradesperson.FirstName, tradesperson.LastName);
                //sp.miles = Distance.BetweenTwoUKPostCodes(postcode, tradesperson.Postcode);
                sp.miles = "1 mile";
                sp.rating = 4;
                serviceproviders.Add(sp);
            }

            return View("SearchResults", serviceproviders);

        }


        public ActionResult ServiceProviderCalendarDetails(string spid)
        {
            int sp_id = Convert.ToInt32(EncryptionUtility.Decrypt(spid.ToString(), true));
            Serviceprovider model = new Serviceprovider();

            return PartialView("_SPCalendarModal", model);
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
