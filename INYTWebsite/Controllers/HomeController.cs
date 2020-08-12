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
using INYTWebsite.CustomModels;
using INYTWebsite.Areas.ServiceProviderArea.Controllers;

namespace INYTWebsite.Controllers
{
    public class HomeController : BaseController
    {
        Repository _repo = null;
        private AppSettings _AppSettings;

        public HomeController(Repository repo, IOptions<AppSettings> settings)
            : base(repo)
        {
            _repo = repo;
            _AppSettings = settings.Value;
        }


        public IActionResult Index()
        {
            BookingModel model = new BookingModel();
            var services = TheRepository.GetServices().ToList();

            var servicesList = services.Select(x => new SelectListItem
            {
                Text = x.Service.ToString(),
                Value = x.id.ToString()
            }).ToList();

            ViewData["Services"] = servicesList;

            return View();
        }


        [HttpPost]
        public ActionResult Search(BookingModel model)
        {
            string apikey = string.Empty;
            apikey = _AppSettings.apikey;

            var services = TheRepository.GetServices();

            ServiceModel serviceModel = services.Where(a => a.id == Convert.ToInt32(model.selectedService)).FirstOrDefault();


            ViewData["postcode"] = model.postCode;
            ViewData["service"] = serviceModel.Service;

            List<ServiceProviderModel> spList = new List<ServiceProviderModel>();
            int miles = 10; //TEMPORARY TO SAVE ON LICENSE
            int spCount = 0;

            var serviceProviders = TheRepository.GetServiceProvidersByService(Convert.ToInt32(model.selectedService)).ToList();
            spCount = serviceProviders.Count();
            ViewData["spCount"] = spCount;

            foreach (var serviceperson in serviceProviders)
            {
                ServiceProviderModel sp = new ServiceProviderModel();
                sp.firstName = serviceperson.FirstName;
                sp.lastName = serviceperson.LastName;
                sp.id = serviceperson.Id;                
                //sp.distanceinmiles = Distance.BetweenTwoUKPostCodes(model.postCode, tradesperson.Postcode, apikey);
                sp.distanceinmiles = String.Format("{0}",miles); miles+=10; //TEMPORARILY TO SAVE ON LICENSE
                sp.rating = 4;
                sp.serviceId = Convert.ToInt32(serviceperson.TradeId);
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
