using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using INYTWebsite.Models;
using INYTWebsite.Model;
using INYTWebsite.Code;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Controllers
{
    public class HomeController : Controller
    {
        public INYTContext _db;
        private AppSettings _AppSettings;

        public HomeController(INYTContext db, IOptions<AppSettings> settings)
        {
            _db = db;
            _AppSettings = settings.Value;
       }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search()
        {
            string postcode = string.Empty;
            postcode = "W13 9XW";
            string trade = string.Empty;
            trade = "Plumbing";

            string apikey = string.Empty;
            apikey = _AppSettings.apikey;

            List<Serviceprovider> spList = new List<Serviceprovider>();
            foreach(var tradesperson in _db.Tradesperson.ToList().Where(a => a.Trade == trade))
            {
                Serviceprovider sp = new Serviceprovider();
                sp.serviceprovidername = String.Format("{0} {1}", tradesperson.FirstName, tradesperson.LastName);
                //sp.miles = Distance.BetweenTwoUKPostCodes(postcode, tradesperson.Postcode, apikey);
                sp.miles = "1 mile";
                sp.rating = 4;
                spList.Add(sp);
            }


            ServiceProviders serviceproviders = new ServiceProviders();
            serviceproviders.serviceProvidersList = spList;


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
