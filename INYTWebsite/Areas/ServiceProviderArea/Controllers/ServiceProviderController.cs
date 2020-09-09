using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

namespace INYTWebsite.Areas.ServiceProvider.Controllers
{
    [Area("ServiceProviderArea")]
    public class ServiceProviderController : BaseController
    {
        Repository _repo = null;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public ServiceProviderController(Repository repo, IOptions<AppSettings> settings, IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
        }

        [Route("index")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult Index()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                ServiceProviderModel model = TheRepository.GetServiceProvider(id);

                if (model != null)
                {
                    model.bookings = TheRepository.GetAllBookings(id);                    
                }

                List<BookingModel> bookingModels = new List<BookingModel>();

                bookingModels = TheRepository.GetAllBookings(model.id);

                model.totalCustomers = bookingModels.Count();
                model.totalCustomersToday = bookingModels.Where(a => a.bookingDate == DateTime.Now).Count();
                model.totalNewBookings = bookingModels.Where(a => a.bookingDate > DateTime.Now).Count();

                return View(model);
            }

            return View();
        }

        [Route("additionalquestions")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult additionalquestions()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                ServiceProviderModel model = TheRepository.GetServiceProvider(Convert.ToInt32(id));
                if (model != null)
                {
                    model.additionalQuestions = TheRepository.GetAdditionalQuestions(id);
                }

                return View(model);
            }

            return View();
        }

        [Route("addquestion")]
        public IActionResult addquestion(ServiceProviderModel model)
        {
            ServiceProviderAdditionalQuestionsModel questionsmodel = new ServiceProviderAdditionalQuestionsModel();
            questionsmodel = model.additionalQuestion;
            questionsmodel.serviceProviderId = model.id;
            TheRepository.CreateAdditionalQuestions(questionsmodel);
            return RedirectToAction("additionalquestions");
        }

        [Route("schedules")]
        [Authorize (Policy = "ServiceProviderOnly")]
        public IActionResult Schedules()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                ServiceProviderModel model = TheRepository.GetServiceProvider(Convert.ToInt32(id));

                if (model != null)
                {
                    model.slots = TheRepository.GetAvailabilitySlots(Convert.ToInt32(id));
                }

                model.newslot = new SlotsModel();

                return View(model);
            }            

            return View();
        }

        [Route("editschedule/{id}")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult EditSchedule(int id)
        {
            SlotsModel model = new SlotsModel();
            model = TheRepository.GetAvailabilitySlotById(id);
            model.minHoursList = GetMinHoursList(model.minimumHours);
            model.startTimeList = GetStartTimeList(model.startTime);
            model.endTimeList = GetEndTimeList(model.endTime);
            return PartialView("_ScheduleEditModal", model);
        }

        [Route("editquestion/{id}")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult EditQuestion(int id)
        {
            ServiceProviderAdditionalQuestionsModel model = new ServiceProviderAdditionalQuestionsModel();
            model = TheRepository.GetServiceProviderQuestion(id);            
            return PartialView("_QuestionEditModal", model);
        }

        [Route("UpdateQuestion")]
        [HttpPost]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult UpdateQuestion(ServiceProviderAdditionalQuestionsModel updatedQuestions)
        {
            ServiceProviderAdditionalQuestionsModel originalQuestions = new ServiceProviderAdditionalQuestionsModel();
            originalQuestions = TheRepository.GetServiceProviderQuestion(updatedQuestions.id);
            TheRepository.UpdateQuestions(originalQuestions, updatedQuestions);
            return RedirectToAction("additionalquestions");
        }

        [Route("DeleteQuestion")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult DeleteQuestion(int id)
        {
            ServiceProviderAdditionalQuestionsModel model = new ServiceProviderAdditionalQuestionsModel();
            TheRepository.DeleteQuestion(id);
            return RedirectToAction("Schedules");
        }

        private IEnumerable<SelectListItem> GetEndTimeList(DateTime endTime)
        {
            List<SelectListItem> endTimes = new List<SelectListItem>();
            endTimes.Add(new SelectListItem { Text="6PM", Value = "6PM"});
            endTimes.Add(new SelectListItem { Text = "7PM", Value = "7PM" });
            endTimes.Add(new SelectListItem { Text = "8PM", Value = "8PM" });
            endTimes.Add(new SelectListItem { Text = "9PM", Value = "9PM" });
            endTimes.Add(new SelectListItem { Text = "10PM", Value = "10PM" });
            return endTimes;
        }

        private IEnumerable<SelectListItem> GetStartTimeList(DateTime startTime)
        {
            List<SelectListItem> startTimes = new List<SelectListItem>();
            startTimes.Add(new SelectListItem { Text = "6AM", Value = "6AM" });
            startTimes.Add(new SelectListItem { Text = "7AM", Value = "7AM" });
            startTimes.Add(new SelectListItem { Text = "8AM", Value = "8AM" });
            startTimes.Add(new SelectListItem { Text = "9AM", Value = "9AM" });
            startTimes.Add(new SelectListItem { Text = "10AM", Value = "10AM" });
            return startTimes;
        }

        private IEnumerable<SelectListItem> GetMinHoursList(int minimumHours)
        {
            List<SelectListItem> minHours = new List<SelectListItem>();
            minHours.Add(new SelectListItem { Text = "1 hr", Value = "1" });
            minHours.Add(new SelectListItem { Text = "2 hrs", Value = "2" });
            minHours.Add(new SelectListItem { Text = "0.5 day", Value = "4" });
            minHours.Add(new SelectListItem { Text = "1 day", Value = "8" });
            return minHours;
        }

        [Route("UpdateSchedule")]
        [HttpPost]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult UpdateSchedule(SlotsModel updatedSlots)
        {
            SlotsModel originalSlots = new SlotsModel();
            originalSlots = TheRepository.GetAvailabilitySlotById(updatedSlots.id);
            TheRepository.UpdateSlot(originalSlots, updatedSlots);
            return RedirectToAction("schedules");
        }

        [Route("DeleteSchedule")]
        [Authorize(Policy = "ServiceProviderOnly")]
        public IActionResult DeleteSchedules(int id)
        {
            SlotsModel model = new SlotsModel();
            TheRepository.DeleteSlot(id);
            return RedirectToAction("Schedules");
        }

        [Route("invoices")]
        public IActionResult Invoices()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                ServiceProviderModel model = TheRepository.GetServiceProvider(Convert.ToInt32(id));

                if (model != null)
                {
                    model.slots = TheRepository.GetAvailabilitySlots(Convert.ToInt32(id));
                }

                return View(model);
            }

            return View();
        }

        [Route("ratings")]
        public IActionResult Ratings()
        {
            int id = this.GetCurrentUserId();
            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            int id = this.GetCurrentUserId();
            if (id > 0)
            {
                ServiceProviderModel model = TheRepository.GetServiceProvider(Convert.ToInt32(id));

                if (model != null)
                {
                    model.slots = TheRepository.GetAvailabilitySlots(Convert.ToInt32(id));
                }

                model.newslot = new SlotsModel();

                return View(model);
            }

            return View();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            HttpContext.Session.Remove("RedirectToCompleteRegistration");

            return RedirectToAction("Index", "Home");

        }


        [HttpPost]
        [Route("addschedule")]
        public IActionResult AddSchedule(ServiceProviderModel model)
        {
            SlotsModel slotsmodel = new SlotsModel();
            slotsmodel = model.newslot;
            slotsmodel.dayOfWeek = Request.Form["dayofweek"].ToString();
            int minimumHours = 0; 
            if (Request.Form["newslot.minimumHours"].ToString() != String.Empty)
            {
                if (Request.Form["newslot.minimumHours"].ToString() == "1 hr")
                    minimumHours = 1;
                else if (Request.Form["newslot.minimumHours"].ToString() == "2 hrs")
                    minimumHours = 2;
                else if (Request.Form["newslot.minimumHours"].ToString() == "0.5 day")
                    minimumHours = 4;
                else if (Request.Form["newslot.minimumHours"].ToString() == "1 day")
                    minimumHours = 8;
            }
            slotsmodel.minimumHours = minimumHours;
            slotsmodel.serviceproviderId = Convert.ToInt32(Request.Form["id"].ToString());
            TheRepository.CreateSlot(slotsmodel);

            return RedirectToAction("schedules");
        }       
    }
}