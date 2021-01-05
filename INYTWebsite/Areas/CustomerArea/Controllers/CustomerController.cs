using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
using Rotativa.AspNetCore;

namespace INYTWebsite.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    public class CustomerController : BaseController
    {
        Repository _repo = null;
        IEmailManager _emailManager;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public CustomerController(Repository repo, IOptions<AppSettings> settings, 
            IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
            _emailManager = emailManager;
        }

        [Route("custindex")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult CustIndex()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                CustomerModel model = TheRepository.GetCustomer(id);

                if (model != null)
                {
                    model.bookings = TheRepository.GetAllBookingsByCustomer(id);
                    model.bookings = model.bookings.Where(a => a.bookingFulfilled == false).ToList();
                }

                List<String> calendarslist = new List<string>();
                foreach(var booking in model.bookings)
                {
                    String calendaritem = CreateCalendar(booking);
                    calendarslist.Add(calendaritem);
                    booking.serviceName = TheRepository.GetServiceById(booking.serviceId).Service.ToString();
                }
                model.calendars = calendarslist;
                return View(model);
            }

            
            return View();
        }


        [Route("taskcomplete")]
        [Authorize(Policy ="CustomerOnly")]
        public IActionResult TaskComplete(int bookingid)
        {
            BookingModel model = new BookingModel();
            model = TheRepository.GetBookingById(bookingid);
            model.bookingFulfilled = true;

            TheRepository.UpdateBooking(model);

            EmailInfo email = new EmailInfo();
            email.Body = String.Format("Booking id {0} has been completed and confirmed by client", model.id);
            email.FromAddress = "anand@futuresolutionsltd.com";
            email.ToAddress = "anand@futuresolutionsltd.com";
            email.Subject = "Booking completed";

            _emailManager.SendEmail(email);

            return RedirectToAction("rateserviceprovider", new {bookingId = model.id });
        }

        [Route("rateserviceprovider")]
        [Authorize(Policy ="CustomerOnly")]
        public IActionResult RateServiceProvider(int bookingId)
        {
            BookingModel bookingModel = new BookingModel();
            bookingModel = TheRepository.GetBookingById(bookingId);

            RatingModel model = new RatingModel();
            model.bookingId = bookingId;
            model.customerId = bookingModel.customerId;

            return View(model);
        }

        [Route("viewinvoice")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult ViewInvoice(int id)
        {
            InvoiceModel newinvoice = new InvoiceModel();
            newinvoice = TheRepository.GetInvoiceById(id);

            newinvoice.serviceprovider = TheRepository.GetServiceProvider(newinvoice.serviceProviderId);
            newinvoice.customer = TheRepository.GetCustomer(newinvoice.customerId);
            newinvoice.bookings = TheRepository.GetAllBookingsByCustomer(newinvoice.customerId).Where(a => a.bookingReference == newinvoice.paypalBookingReference).ToList();

            foreach (var booking in newinvoice.bookings)
            {
                booking.serviceName = TheRepository.GetServiceById(booking.serviceId).Service;
            }

            return View(newinvoice);
        }

        [Route("printinvoice")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult PrintInvoice()
        {
            InvoiceModel model = new InvoiceModel();
            model = TheRepository.GetInvoiceById(Convert.ToInt32(Request.Form["id"]));
            model.serviceprovider = TheRepository.GetServiceProvider(model.serviceProviderId);
            model.customer = TheRepository.GetCustomer(model.customerId);
            model.bookings = TheRepository.GetAllBookingsByCustomer(model.customerId).Where(a => a.bookingReference == model.paypalBookingReference).ToList();

            foreach (var booking in model.bookings)
            {
                booking.serviceName = TheRepository.GetServiceById(booking.serviceId).Service;
            }

            return new ViewAsPdf("PrintInvoice", model);
        }

        [Route("submitratings")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult SubmitRatings(RatingModel model)
        {
            model.createdBy = 3;
            model.createdDate = DateTime.Now;
            TheRepository.CreateRating(model);
            return RedirectToAction("custindex");
        }

        [Route("custinvoices")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult Invoices()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                CustomerModel model = TheRepository.GetCustomer(Convert.ToInt32(id));

                if (model != null)
                {
                    model.invoices = TheRepository.GetInvoicesByCustomer(Convert.ToInt32(id));
                }

                return View(model);
            }

            return View();
        }

        [Route("custratings")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult Ratings()
        {
            int id = this.GetCurrentUserId();
            return View();
        }

        [Route("custprofile")]
        [Authorize(Policy = "CustomerOnly")]
        public IActionResult Profile()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                CustomerModel model = TheRepository.GetCustomer(Convert.ToInt32(id));
                return View(model);
            }

            return View();
        }

        [HttpGet]
        [Route("custlogout")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            HttpContext.Session.Remove("RedirectToCompleteRegistration");

            return RedirectToAction("Index", "Home");

        }

        private string CreateCalendar(BookingModel model)
        {
            StringBuilder sb = new StringBuilder();
            string DateFormat = "yyyyMMddTHHmmssZ";
            string now = model.bookingDate.ToString(DateFormat);
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Compnay Inc//Product Application//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");
            DateTime dtStart = Convert.ToDateTime(model.bookingTime);
            DateTime dtEnd = Convert.ToDateTime(model.bookingTime);
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine("DTSTART:" + dtStart.ToUniversalTime().ToString(DateFormat));
            sb.AppendLine("DTEND:" + dtEnd.ToUniversalTime().ToString(DateFormat));
            sb.AppendLine("DTSTAMP:" + now);
            sb.AppendLine("UID:" + Guid.NewGuid());
            sb.AppendLine("CREATED:" + now);
            sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + model.serviceProvider.firstName);
            sb.AppendLine("DESCRIPTION:" + model.selectedService);
            sb.AppendLine("LAST-MODIFIED:" + now);
            //sb.AppendLine("LOCATION:" + model.eventLocation);
            sb.AppendLine("SEQUENCE:0");
            sb.AppendLine("STATUS:CONFIRMED");
            //sb.AppendLine("SUMMARY:" + res.Summary);
            sb.AppendLine("TRANSP:OPAQUE");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();
        }
    }
}