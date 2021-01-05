using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using INYTWebsite.Areas.AdminArea.Models;
using INYTWebsite.Areas.ServiceProviderArea.Controllers;
using INYTWebsite.Code;
using INYTWebsite.CustomModels;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AdminHomeController : BaseController
    {
        Repository _repo = null;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public AdminHomeController(Repository repo, IOptions<AppSettings> settings, IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            //Get number of customers
            var custquery = TheRepository.GetCustomers();
            int customers = custquery.Count();

            //Get number of service providers
            var spquery = TheRepository.GetServiceProviders();
            int serviceproviders = spquery.Count();


            // Get number of bookings
            var bookingsquery = TheRepository.GetBookings();
            int bookings = bookingsquery.Count();

            //Get total revenue - including split of commission
            double totalamount = bookingsquery.Select(a => a.bookingAmount).Sum();

            AdminHomeModel model = new AdminHomeModel();
            model.bookingsamount = totalamount;
            model.bookingscount = bookings;
            model.customercount = customers;
            model.serviceprovidercount = serviceproviders;

            return View(model);
        }

        [Route("Bookings")]
        public IActionResult Bookings()
        {
            var model = TheRepository.GetBookings();

            foreach(var booking in model)
            {
                booking.customer = TheRepository.GetCustomer(booking.customerId);
                booking.serviceProvider = TheRepository.GetServiceProvider(booking.serviceProviderId);
            }
            return View(model);
        }

        [Route("ratings")]
        public IActionResult Ratings()
        {
            List<RatingModel> ratings = new List<RatingModel>();
            ratings = TheRepository.GetAllRatings();
            foreach(var rating in ratings)
            {
                BookingModel bookingModel = new BookingModel();
                bookingModel = TheRepository.GetBookingById(rating.bookingId);
                if (bookingModel != null)
                {
                    rating.serviceprovider = TheRepository.GetServiceProvider(bookingModel.serviceProviderId);
                }
                rating.customer = TheRepository.GetCustomer(rating.customerId);
            }

            return View(ratings);
        }

        [Route("Services")]
        public IActionResult Services()
        {
            var model = TheRepository.GetServices();
            return View(model);
        }

        [Route("ServiceProviders")]
        public IActionResult ServiceProviders()
        {
            var model = TheRepository.GetServiceProviders();
            return View(model);
        }

        [Route("Customers")]
        public IActionResult Customers()
        {
            var model = TheRepository.GetCustomers();
            return View(model);
        }

        [Route("addservice")]
        public IActionResult AddService(ServiceModel model)
        {
            TheRepository.CreateService(model);
            return RedirectToAction("services");

        }

        [Route("editservice/{id}")]
        public IActionResult EditService(int id)
        {
            ServiceModel model = new ServiceModel();
            model = TheRepository.GetServiceById(id);
            return PartialView("_ServiceEditModal", model);
        }

        [Route("UpdateService")]
        [HttpPost]
        public IActionResult UpdateService(ServiceModel updatedService)
        {
            ServiceModel originalService = new ServiceModel();
            originalService = TheRepository.GetServiceById(updatedService.id);
            TheRepository.UpdateService(originalService, updatedService);
            return RedirectToAction("services");
        }

        [Route("DeleteService")]
        public IActionResult DeleteQuestion(int id)
        {
            ServiceModel model = new ServiceModel();
            TheRepository.DeleteService(id);
            return RedirectToAction("services");
        }
    }
}