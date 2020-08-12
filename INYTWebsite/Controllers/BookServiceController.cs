using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Areas.ServiceProviderArea.Controllers;
using INYTWebsite.Code;
using INYTWebsite.CustomModels;
using INYTWebsite.Extensions;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Controllers
{

    public class BookServiceController : BaseController
    {
        Repository _repo = null;
        private AppSettings _AppSettings;

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

        [HttpGet ("BookService/{id}")]
        public IActionResult Index(string id, string postcode)
        {
            ServiceProviderModel serviceProvider = new ServiceProviderModel();
            serviceProvider = TheRepository.GetServiceProvider(Convert.ToInt32(id));
            var questionsModel = TheRepository.GetAdditionalQuestions(serviceProvider.id);

            //Get the customer address from the postcode here
            CustomerModel customer = new CustomerModel();
            customer.postcode = postcode;
            
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

        public IActionResult BookStep1(BookingModel model)
        {
            model.serviceProvider = TheRepository.GetServiceProvider(model.serviceProviderId);
            model.serviceName = TheRepository.GetServices().Where(a => a.id == model.serviceId).FirstOrDefault().Service;

            List<AdditionalQuestionsModel> questionsModel = new List<AdditionalQuestionsModel>();

            foreach(var key in Request.Form.Keys)
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

            List<AvailabilityDatesModel> availabilityDates = new List<AvailabilityDatesModel>();
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            DayOfWeek fdow = ci.DateTimeFormat.FirstDayOfWeek;
            DayOfWeek today = DateTime.Now.DayOfWeek;
            DateTime sow = DateTime.Now.AddDays(-(today - fdow)).Date;
            DateTime eow = sow.AddDays(7).Date;
            int interval = 1;
            availabilityDates = TheRepository.GetAvailabilityDates(model.serviceProviderId, sow, eow, interval);

            var sortedList = availabilityDates.ToList().OrderBy(a => a.availabilityDate.TimeOfDay);

            var minstarttime = sortedList.First().startTime;
            var maxstarttime = sortedList.Last().endTime;

            availabilityDates = availabilityDates.Where(a => a.availabilityDate.TimeOfDay >= minstarttime.TimeOfDay
                                                                && a.availabilityDate.TimeOfDay <= maxstarttime.TimeOfDay).ToList();

            model.availabilityDates = availabilityDates;

            List<CalendarDates> calendar = new List<CalendarDates>();

            foreach (var item in availabilityDates)
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


            //int customerid = 0;

            //if (TheRepository.GetCustomerByEmail(model.customer.emailAddress) != null)
            //{
            //    var cust = TheRepository.CreateCustomer(model.customer);
            //    customerid = cust.id;
            //}
            //else
            //{
            //    customerid = TheRepository.GetCustomerByEmail(model.customer.emailAddress).id;
            //}

            //var newBooking = TheRepository.CreateBooking(model);
            //int newBookingId = newBooking.id;
            //if (newBooking != null)
            //{
            //    foreach (var answer in model.questionsList)
            //    {
            //        TheRepository.CreateAdditionalQuestions(answer);
            //    }
            //}

            //ViewData["newBookingId"] = newBookingId;
            return View(model);
        }

        public ActionResult BookStep2(BookingModel model)
        {
            var selectedTimes = Request.Form["selectedTimes"].ToString();

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
                    var newBooking = TheRepository.CreateBooking(model);
                    model.id = newBooking.id;
                }
            }
            
            return View("ConfirmPayment", model);
        }
    }
}