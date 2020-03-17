using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using INYTWebsite.Models;
using INYTWebsite.Code;
using Microsoft.Extensions.Options;
using INYTWebsite.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            BookingModel model = new BookingModel();
            var tradesList = _db.TradeMaster.ToList().Select(x => new SelectListItem
            {
                Text = x.Trade.ToString(),
                Value = x.Id.ToString()
            }).ToList();

            ViewData["Trades"] = tradesList;

            return View();
        }


        [HttpPost]
        public ActionResult Search(BookingModel model)
        {
            string apikey = string.Empty;
            apikey = _AppSettings.apikey;

            TradeMaster tradeMaster = new TradeMaster();
            tradeMaster = _db.TradeMaster.Where(a => a.Id == Convert.ToInt32(model.selectedTrade)).FirstOrDefault();


            ViewData["postcode"] = model.postCode;
            ViewData["trade"] = tradeMaster.Trade;

            List<ServiceProviderModel> spList = new List<ServiceProviderModel>();
            int miles = 10; //TEMPORARY TO SAVE ON LICENSE
            int spCount = 0;

            List<Tradesperson> serviceProviders = new List<Tradesperson>();
            if (_db.Tradesperson.ToList().Count > 0)
            {
                serviceProviders = _db.Tradesperson.Where(a => a.TradeId == Convert.ToInt32(model.selectedTrade)).ToList();
                spCount = serviceProviders.Count();
                ViewData["spCount"] = spCount;
            }

            foreach (var tradesperson in serviceProviders)
            {
                ServiceProviderModel sp = new ServiceProviderModel();
                sp.firstName = tradesperson.FirstName;
                sp.lastName = tradesperson.LastName;
                sp.id = tradesperson.Id;                
                //sp.distanceinmiles = Distance.BetweenTwoUKPostCodes(model.postCode, tradesperson.Postcode, apikey);
                sp.distanceinmiles = String.Format("{0}",miles); miles+=10; //TEMPORARILY TO SAVE ON LICENSE
                sp.rating = 4;
                sp.tradeId = Convert.ToInt32(tradesperson.TradeId);
                spList.Add(sp);
            }

            return View("SearchResults", spList);

        }


        public ActionResult ServiceProviderCalendarDetails(string spid)
        {
            int sp_id = Convert.ToInt32(EncryptionUtility.Decrypt(spid.ToString(), true));
            ServiceProviderModel model = new ServiceProviderModel();

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
