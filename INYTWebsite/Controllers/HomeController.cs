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
using System.Net.Http;

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
            apikey = _AppSettings.GoogleAPI.distanceAPI;

            var services = TheRepository.GetServices();

            ServiceModel serviceModel = services.Where(a => a.id == Convert.ToInt32(model.selectedService)).FirstOrDefault();

            model.postCode = Request.Form["postal_code"];
            CustomerAddress custaddress = new CustomerAddress()
            {
                country = Request.Form["country"],
                housenumber = Request.Form["street_number"],
                postcode = Request.Form["postal_code"],
                state = Request.Form["administrative_area_level_1"],
                streetname = Request.Form["route"]
            };

            Response.Cookies.Append("country", custaddress.country);
            Response.Cookies.Append("housenumber", custaddress.housenumber);
            Response.Cookies.Append("postcode", custaddress.postcode);
            Response.Cookies.Append("state", custaddress.state);
            Response.Cookies.Append("streetname", custaddress.streetname);

            ViewData["postcode"] = model.postCode;
            ViewData["service"] = serviceModel.Service;

            int spCount = 0;
            var serviceProviders = TheRepository.GetServiceProvidersByService(Convert.ToInt32(model.selectedService)).ToList();
            spCount = serviceProviders.Count();
            ViewData["spCount"] = spCount;

            foreach (var serviceperson in serviceProviders)
            {
                serviceperson.distanceinmiles = Distance.BetweenTwoUKPostCodes(model.postCode, serviceperson.postcode, apikey);

                var ratings = TheRepository.GetRatings(serviceperson.id);

                if ((ratings != null) && (ratings.Count > 0))
                {
                    serviceperson.rating = ratings.Average(a => a.ratings);
                }

                serviceperson.ratings = ratings;
                serviceperson.service = TheRepository.GetServiceById(serviceperson.serviceId).Service;
            }

            return View("SearchResults", serviceProviders);
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
