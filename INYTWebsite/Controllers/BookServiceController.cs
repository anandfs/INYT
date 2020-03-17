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
        public IActionResult Index(string id)
        {
            Tradesperson model = new Tradesperson();
            model = _db.Tradesperson.Find(Convert.ToInt32(id));

            ServiceProviderModel spModel = new ServiceProviderModel();
            spModel.addressLine1 = model.AddressLine1;
            spModel.addressLine2 = model.AddressLine2;
            spModel.city = model.City;
            spModel.companyNumber = model.CompanyNumber;
            spModel.companySize = model.CompanySize;
            spModel.contactNumber = model.ContactNumber;
            spModel.country = model.Country;
            spModel.deactivationReason = model.DeactivationReason;
            spModel.description = model.Description;
            spModel.firstName = model.FirstName;
            spModel.lastName = model.LastName;
            spModel.postcode = model.Postcode;
            spModel.region = model.Region;
            spModel.tradeId = Convert.ToInt32(model.TradeId);            
            spModel.website = model.Website;

            List<TradeAdditionalQuestions> questions = new List<TradeAdditionalQuestions>();
            questions = _db.TradeAdditionalQuestions.Where(a => a.TradeId == spModel.tradeId).ToList();
            spModel.questionsList = questions;

            return View(spModel);
        }
    }
}