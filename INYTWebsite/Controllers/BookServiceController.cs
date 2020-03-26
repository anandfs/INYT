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

            CustomerModel customer = new CustomerModel();
            customer.postcode = postcode;
            
            //Get the customer address from the postcode here


            BookingModel model = new BookingModel();
            model.questionsList = questions;
            model.customer = customer;
            model.serviceProvider = serviceprovider;

            return View(model);
        }

        public IActionResult BookStep1(BookingModel model)
        {
            return View(model);
        }
    }
}