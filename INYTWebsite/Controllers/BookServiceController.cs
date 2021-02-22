using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using INYTWebsite.Areas.ServiceProviderArea.Controllers;
using INYTWebsite.Code;
using INYTWebsite.CustomModels;
using INYTWebsite.Extensions;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PayPal;
using PayPal.OpenIdConnect;

using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using BraintreeHttp;
using Rotativa.AspNetCore;

namespace INYTWebsite.Controllers
{

    public class BookServiceController : BaseController
    {
        Repository _repo = null;
        private AppSettings _AppSettings;

        private Tokeninfo info;
        private bool UseSandbox;
        private readonly object _env;


        public BookServiceController(Repository repo, IOptions<AppSettings> settings)
             : base(repo)
        {
            _repo = repo;
            _AppSettings = settings.Value;
        }

        public JsonResult GetAvailableDates(int serviceProviderId, DateTime startDate, DateTime endDate)
        {
            string totalcost = "";
            return Json(totalcost);
        }

        [HttpGet("BookService/{id}")]
        public IActionResult Index(string id, string postcode, int? customerid)
        {
            ServiceProviderModel serviceProvider = new ServiceProviderModel();
            serviceProvider = TheRepository.GetServiceProvider(Convert.ToInt32(id));
            var questionsModel = TheRepository.GetAdditionalQuestions(serviceProvider.id);

            CustomerAddress custaddress = new CustomerAddress();
            CustomerModel customer = new CustomerModel();

            if (customerid != null)
            {
                customer = TheRepository.GetCustomer(Convert.ToInt32(customerid));
                custaddress.housenumber = customer.addressLine1;
                custaddress.streetname = customer.addressLine2;
                custaddress.country = customer.country;
                custaddress.state = customer.region;
                custaddress.postcode = customer.postcode;
            }
            else
            {
                custaddress.country = Request.Cookies["country"];
                custaddress.housenumber = Request.Cookies["housenumber"];
                custaddress.postcode = Request.Cookies["postcode"];
                custaddress.state = Request.Cookies["state"];
                custaddress.streetname = Request.Cookies["streetname"];


                //Get the customer address from the postcode here
                customer.postcode = postcode;
                customer.addressLine1 = string.Format("{0}, {1}", custaddress.housenumber, custaddress.streetname);
                customer.country = custaddress.country;
                customer.region = custaddress.state;
            }

            BookingModel model = new BookingModel();
            model.questionsList = questionsModel;
            model.customer = customer;
            model.serviceProviderId = serviceProvider.id;
            model.serviceProvider = serviceProvider;
            model.serviceId = serviceProvider.serviceId;
            model.bookingDate = DateTime.Now;
            model.bookingTime = DateTime.Now;
            return View(model);
        }

        [HttpGet("BookService/Ratings/{id}")]
        public IActionResult Ratings(string id)
        {
            ServiceProviderModel model = new ServiceProviderModel();
            model = TheRepository.GetServiceProvider(Convert.ToInt32(id));

            var ratings = TheRepository.GetRatings(model.id);

            if ((ratings != null) && (ratings.Count > 0))
            {
                model.rating = ratings.Average(a => a.ratings);
                
            }
            model.ratings = ratings;

            foreach(var rating in ratings)
            {
                rating.customer = TheRepository.GetCustomer(rating.customerId);
            }

            return PartialView("_Ratings", model);
        }

        public IActionResult BookStep1(BookingModel model, DateTime? startDate)
        {
            model.serviceProvider = TheRepository.GetServiceProvider(model.serviceProviderId);
            model.serviceName = TheRepository.GetServices().Where(a => a.id == model.serviceId).FirstOrDefault().Service;

            List<AdditionalQuestionsModel> questionsModel = new List<AdditionalQuestionsModel>();

            foreach (var key in Request.Form.Keys)
            {
                if (key.StartsWith("txa") || key.StartsWith("rdo") || key.StartsWith("chk"))
                {
                    AdditionalQuestionsModel questionModel = new AdditionalQuestionsModel();

                    string field = key.Substring(0, 3);
                    questionModel.id = Convert.ToInt32(key.Substring(4, (key.Length - 4)));

                    ServiceProviderAdditionalQuestionsModel additionalQuestion = new ServiceProviderAdditionalQuestionsModel();
                    additionalQuestion = TheRepository.GetServiceProviderQuestion(questionModel.id);

                    questionModel.additionalQuestion = additionalQuestion.additionalQuestion;
                    questionModel.serviceId = Convert.ToInt32(model.serviceId);
                    questionModel.answer = Request.Form[String.Format("{0}_{1}", field, questionModel.id)].ToString();
                    questionsModel.Add(questionModel);
                }
            }

            model.additionalQuestionsList = questionsModel;
            model.availabilityDates = GetAvailabilityDates(model.serviceProviderId, startDate);
            List<CalendarDates> calendar = new List<CalendarDates>();


            foreach (var item in model.availabilityDates)
            {
                if (!calendar.Any(a => a.weekdate.Date == item.availabilityDate.Date))
                {
                    calendar.Add(new CalendarDates
                    {
                        weekdate = item.availabilityDate.Date,
                        weekname = item.weekName
                    });
                }
            }

            model.calendar = calendar;

            HttpContext.Session.SetObject("bookingmodel", model);

            return View(model);
        }

        [HttpGet("BookService/GetCalendar/{serviceProviderId}/{startDate}")]
        public ActionResult GetCalendar(int serviceProviderId, string startDate)
        {
            BookingModel bookingModel = new BookingModel();
            bookingModel.serviceProviderId = serviceProviderId;
            bookingModel.availabilityDates = GetAvailabilityDates(serviceProviderId, Convert.ToDateTime(startDate));

            List<CalendarDates> calendar = new List<CalendarDates>();


            foreach (var item in bookingModel.availabilityDates)
            {
                if (!calendar.Any(a => a.weekdate.Date == item.availabilityDate.Date))
                {
                    calendar.Add(new CalendarDates
                    {
                        weekdate = item.availabilityDate.Date,
                        weekname = item.weekName
                    });
                }
            }

            bookingModel.calendar = calendar;
            return PartialView("_Calendar", bookingModel);
        }

        private List<AvailabilityDatesModel> GetAvailabilityDates(int serviceProviderId, DateTime? startDate)
        {
            List<AvailabilityDatesModel> availabilityDates = new List<AvailabilityDatesModel>();
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;

            DayOfWeek today = DateTime.Now.DayOfWeek;
            DateTime sow;

            if (startDate != null)
            {
                sow = Convert.ToDateTime(startDate);
            }
            else
            {
                sow = DateTime.Now.Date; // DateTime.Now.AddDays(-(today - fdow)).Date;
            }

            DateTime eow = sow.AddDays(8).Date;
            int interval = 1;
            availabilityDates = TheRepository.GetAvailabilityDates(serviceProviderId, sow, eow, interval);

            if (availabilityDates.Count > 0)
            {
                var sortedList = availabilityDates.ToList().OrderBy(a => a.availabilityDate.TimeOfDay);

                var minstarttime = sortedList.First().startTime;
                var maxstarttime = sortedList.Last().endTime;

                availabilityDates = availabilityDates.Where(a => a.availabilityDate.TimeOfDay >= minstarttime.TimeOfDay
                                                                    && a.availabilityDate.TimeOfDay <= maxstarttime.TimeOfDay).ToList();

            }

            return availabilityDates;
        }

        public ActionResult BookStep2(BookingModel model)
        {
            var selectedTimes = Request.Form["selectedTimes"].ToString();

            BookingsListModel bookingsList = new BookingsListModel();

            foreach(var selectedTime in selectedTimes.Split(','))
            {
                if (!String.IsNullOrEmpty(selectedTime))
                {
                    var bookingAmount = Convert.ToInt32(selectedTime.Split('_')[0].ToString());
                    var requestedDate = Convert.ToDateTime(selectedTime.Split('_')[1].ToString());
                    var requestedTime = Convert.ToDateTime(selectedTime.Split('_')[2].ToString().Replace('-',':'));
                    var requestedDay = selectedTime.Split('_')[2].ToString();

                    model.bookingDate = new DateTime(requestedDate.Year, requestedDate.Month, requestedDate.Day, requestedTime.Hour, requestedTime.Minute, requestedTime.Second);
                    model.bookingTime = new DateTime(requestedDate.Year, requestedDate.Month, requestedDate.Day, requestedTime.Hour, requestedTime.Minute, requestedTime.Second);
                    model.bookingAmount = bookingAmount;
                    model.serviceProviderId = Convert.ToInt32(Request.Form["serviceProviderId"]);
                    model.serviceId = Convert.ToInt32(Request.Form["serviceId"]);

                    int hours = TheRepository.GetMinHours(model.serviceProviderId, requestedDate.DayOfWeek.ToString());

                    int customerid = 0;

                    if (TheRepository.GetCustomerByEmail(model.customer.emailAddress) == null)
                    {
                        var cust = TheRepository.CreateCustomer(model.customer);
                        customerid = cust.id;
                    }
                    else
                    {
                        customerid = TheRepository.GetCustomerByEmail(model.customer.emailAddress).id;
                    }

                    model.customerId = customerid;
                    model.bookingHours = hours;

                    var newBooking = TheRepository.CreateBooking(model);
                    int newBookingId = newBooking.id;
                    if (newBooking != null)
                    {
                        foreach (var answer in model.additionalQuestionsList)
                        {
                            answer.customerId = customerid;
                            answer.serviceId = model.serviceId;
                            TheRepository.CreateAdditionalQuestions(answer);
                        }
                    }

                    model.id = newBooking.id;
                }
            }

            List<BookingModel> bookings = new List<BookingModel>();
            bookings = TheRepository.GetAllBookingsByCustomer(model.customerId);
            bookingsList.bookings = bookings.Where(a => a.bookingAccepted == false).ToList();
            bookingsList.serviceProvider = TheRepository.GetServiceProvider(model.serviceProviderId);
            return View("ConfirmPayment", bookingsList);
        }

        public ActionResult ThankYou(int id, string referenceid)
        {
            BookingsListModel model = new BookingsListModel();
            List<BookingModel> bookings = new List<BookingModel>(); 
            bookings = TheRepository.GetAllBookingsByCustomer(id).Where(a => a.bookingReference == referenceid).ToList();
            bookings = bookings.Where(a => a.bookingFulfilled == false).ToList();
            model.bookings = bookings;
            model.serviceProvider = TheRepository.GetServiceProvider(model.bookings[0].serviceProviderId);
            model.customer = TheRepository.GetCustomer(id);

            //Send an authorisation email to the customer
            EmailInfo emailInfo = new EmailInfo();
            emailInfo.Body = $"Welcome from I NEED YOUR TIME. Please see below for details of the appointment";
            emailInfo.emailType = "Appointment confirmation";
            emailInfo.IsBodyHtml = true;
            emailInfo.Subject = "Welcome to INYT";
            emailInfo.ToAddress = model.customer.emailAddress;
            //_emailManager.SendEmail(emailInfo);
            var model2 = new Emailmodel
            {
                Name = model.customer.firstName,
                Body = emailInfo.Body
            };
            var renderedHTML = ControllerExtensions.RenderViewAsHTMLString(this, "_VerifyEmail.cshtml", model2);
            EmailManager.SendEmail2(model.customer.emailAddress, "VerifyAccount", renderedHTML.Result);

            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    booking.bookingFulfilled = true;
                    booking.bookingAccepted = true;
                    booking.bookingReference = referenceid;
                    TheRepository.UpdateBooking(booking);
                }
            }

            return View(model);
        }

        public ActionResult CreateInvoice()
        {
            double bookingAmount = Convert.ToDouble(Request.Form["totalamount"]);
            string invoiceNumber = String.Empty;
            int lastId = 0;
            var invoiceCount = TheRepository.GetAllInvoices().Count();
            if (invoiceCount > 0)
            {
                lastId = TheRepository.GetAllInvoices().Max(a => a.id);
            }
            else
            {
                lastId = 1;
            }

            invoiceNumber = String.Format("INYTINV{0}", lastId);
            InvoiceModel invoiceModel = new InvoiceModel();
            invoiceModel.customerId = Convert.ToInt32(Request.Form["customerId"]);
            invoiceModel.serviceProviderId = Convert.ToInt32(Request.Form["serviceproviderid"]);
            invoiceModel.paypalBookingReference = Request.Form["reference"];
            invoiceModel.amount = bookingAmount;
            invoiceModel.invoiceNumber = invoiceNumber;
            invoiceModel.paidDate = DateTime.Now;


            InvoiceModel newinvoice = new InvoiceModel();
            newinvoice = TheRepository.CreateInvoice(invoiceModel);

            newinvoice.serviceprovider = TheRepository.GetServiceProvider(invoiceModel.serviceProviderId);
            newinvoice.customer = TheRepository.GetCustomer(invoiceModel.customerId);
            newinvoice.bookings = TheRepository.GetAllBookingsByCustomer(invoiceModel.customerId).Where(a => a.bookingReference == invoiceModel.paypalBookingReference).ToList();

            foreach(var booking in newinvoice.bookings)
            {
                booking.serviceName = TheRepository.GetServiceById(booking.serviceId).Service;
            }


            InvoiceModel model = new InvoiceModel();
            model = TheRepository.GetInvoiceById(newinvoice.id);
            model.serviceprovider = TheRepository.GetServiceProvider(model.serviceProviderId);
            model.customer = TheRepository.GetCustomer(model.customerId);
            model.bookings = TheRepository.GetAllBookingsByCustomer(model.customerId).Where(a => a.bookingReference == model.paypalBookingReference).ToList();

            foreach (var booking in model.bookings)
            {
                booking.serviceName = TheRepository.GetServiceById(booking.serviceId).Service;
            }

            var renderedHTML = ControllerExtensions.RenderViewAsHTMLStringForInvoice(this, "PrintInvoice.cshtml", model);
            var cus = TheRepository.GetCustomer(newinvoice.customerId);
            EmailManager.SendEmail2(cus.emailAddress, "Invoice", renderedHTML.Result);

            return View(newinvoice);
        }


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

        public async static Task<PayPalHttp.HttpResponse> CreatePaypalOrder(string OrderId, bool debug = false)
        {
            var request = new OrdersCaptureRequest(OrderId);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            //3. Call PayPal to capture an order
            var response = await PayPalClient.client().Execute(request);
            //4. Save the capture ID to your database. Implement logic to save capture to your database for future reference.
            if (debug)
            {
                var result = response.Result<Order>();
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
                Console.WriteLine("Capture Ids: ");
                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    foreach (Capture capture in purchaseUnit.Payments.Captures)
                    {
                        Console.WriteLine("\t {0}", capture.Id);
                    }
                }
                AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
            }

            return response;
        }


        public async static Task<PayPalHttp.HttpResponse> CapturePaypal(string OrderId, bool debug = false)
        {
            var request = new OrdersCaptureRequest(OrderId);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            //3. Call PayPal to capture an order
            var response = await PayPalClient.client().Execute(request);
            //4. Save the capture ID to your database. Implement logic to save capture to your database for future reference.
            if (debug)
            {
                var result = response.Result<Order>();
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
                Console.WriteLine("Capture Ids: ");
                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    foreach (Capture capture in purchaseUnit.Payments.Captures)
                    {
                        Console.WriteLine("\t {0}", capture.Id);
                    }
                }
                AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                //Console.WriteLine("Buyer:");
                //Console.WriteLine("\tEmail Address: {0}\n\tName: {1}\n\tPhone Number: {2}{3}", result.Payer.Email, result.Payer.Name.FullName, result.Payer.PhoneWithType.PhoneNumber.CountryCallingCode, result.Payer.PhoneWithType.PhoneNumber.NationalNumber);
            }

            return response;
        }

        public JsonResult CompleteBooking(string refid, int customerid)
        {
            List<BookingModel> bookings = new List<BookingModel>();
            bookings = TheRepository.GetAllBookingsByCustomer(customerid);


            if (bookings != null)
            {
                foreach(var booking in bookings)
                {
                    booking.bookingFulfilled = false;
                    booking.bookingAccepted = true;
                    booking.bookingReference = refid;
                    TheRepository.UpdateBooking(booking);
                }
            }
            //return Json(true);
            return Json(new { result = "Redirect", url = Url.Action("ThankYou", "BookService", new { id = customerid, referenceid=refid }) });

        }
    }
}