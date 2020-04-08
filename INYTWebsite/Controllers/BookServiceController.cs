using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Controllers
{

    public class BookServiceController : Controller
    {
        public INYTContext _db;
        private AppSettings _AppSettings;

        public BookServiceController(INYTContext db, IOptions<AppSettings> settings)
        {
            _db = db;
            _AppSettings = settings.Value;
        }

        [HttpGet ("BookService/{id}")]
        public IActionResult Index(string id, string postcode)
        {
            Tradesperson serviceprovider = new Tradesperson();
            serviceprovider = _db.Tradesperson.Find(Convert.ToInt32(id));

            List<TradeAdditionalQuestions> questions = new List<TradeAdditionalQuestions>();
            questions = _db.TradeAdditionalQuestions.Where(a => a.TradeId == Convert.ToInt32(serviceprovider.TradeId)).ToList();

            List<AdditionalQuestionsModel> questionsModel = new List<AdditionalQuestionsModel>();

            foreach(var question in questions)
            {
                AdditionalQuestionsModel questionModel = new AdditionalQuestionsModel
                {
                    additionalQuestion = question.AdditionalQuestion,
                    answerOptions = question.AnswerOptions,
                    answeroptionType = question.AnswerOptionType,
                    questionId = question.Id,
                    tradeId = Convert.ToInt32(question.TradeId)
                };

                questionsModel.Add(questionModel);
            }

            CustomerModel customer = new CustomerModel();
            customer.postcode = postcode;
            
            //Get the customer address from the postcode here


            BookingModel model = new BookingModel();
            model.questionsList = questionsModel;
            model.customer = customer;
            model.serviceProviderId = serviceprovider.Id;
            model.bookingDate = DateTime.Now;
            model.bookingTime = DateTime.Now;

            return View(model);
        }

        public IActionResult BookStep1(BookingModel model)
        {
            List<AdditionalQuestionsModel> questionsModel = new List<AdditionalQuestionsModel>();

            foreach(var key in Request.Form.Keys)
            {
                if (key.StartsWith("txa") || key.StartsWith("rdo") || key.StartsWith("chk"))
                {
                    AdditionalQuestionsModel questionModel = new AdditionalQuestionsModel();

                    string field = key.Substring(0, 3);
                    questionModel.questionId = Convert.ToInt32(key.Substring(4, (key.Length - 4)));

                    TradeAdditionalQuestions additionalQuestion = new TradeAdditionalQuestions();
                    additionalQuestion = _db.TradeAdditionalQuestions.Where(a => a.Id == questionModel.questionId).FirstOrDefault();

                    questionModel.additionalQuestion = additionalQuestion.AdditionalQuestion;
                    questionModel.tradeId = Convert.ToInt32(additionalQuestion.TradeId);
                    questionModel.answer = Request.Form[String.Format("{0}_{1}", field, questionModel.questionId)].ToString();
                    questionsModel.Add(questionModel);
                }
            }

            model.questionsList = questionsModel;

            int customerid = 0;

            if (!_db.CustomerRegistration.Any(a => a.EmailAddress == model.customer.emailAddress))
            {
                CustomerRegistration cust = new CustomerRegistration();
                cust.AddressLine1 = model.customer.addressLine1;
                cust.AddressLine2 = model.customer.addressLine2;
                cust.City = model.customer.city;
                cust.ContactNumber = model.customer.contactNumber;
                cust.Country = model.customer.country;
                cust.EmailAddress = model.customer.emailAddress;
                cust.FirstName = model.customer.firstName;
                cust.HasAgreedTc = true;
                cust.LastName = model.customer.lastName;
                cust.Postcode = model.customer.postcode;
                cust.Region = model.customer.region;

                _db.CustomerRegistration.Add(cust);
                _db.SaveChanges();

                customerid = cust.Id;
            }
            else
            {
                customerid = _db.CustomerRegistration.Where(a => a.EmailAddress == model.customer.emailAddress).FirstOrDefault().Id;
            }

            //Create a temporary booking
            Booking newBooking = new Booking();

            newBooking.BookingAmount = 0;
            newBooking.BookingDate = model.bookingDate;
            newBooking.BookingFulfilled = false;
            newBooking.BookingPaymentType = String.Empty;
            newBooking.BookingTime = model.bookingTime;
            newBooking.CustomerId = customerid;
            newBooking.TradeId = model.tradeId;
            newBooking.TradespersonId = model.serviceProviderId;

            _db.Booking.Add(newBooking);
            _db.SaveChanges();

            int newBookingId = newBooking.Id;

            foreach(var answer in model.questionsList)
            {
                AdditionalQuestionAnswers AdditionalAnswer = new AdditionalQuestionAnswers()
                {
                    AdditionalQuestion = answer.additionalQuestion,
                    Answer = answer.answer,
                    BookingId = newBookingId,
                    CustomerId = model.customer.id,
                    TradeId = model.tradeId
                };
                _db.AdditionalQuestionAnswers.Add(AdditionalAnswer);
                _db.SaveChanges();
            }

            ViewData["newBookingId"] = newBookingId;

            return View(model);
        }
    }
}