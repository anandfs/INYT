using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Areas.ServiceProviderArea.Controllers;
using INYTWebsite.Code;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Controllers
{
    public class ServicesController : BaseController
    {
        Repository _repo = null;
        private AppSettings _AppSettings;

        public ServicesController(Repository repo, IOptions<AppSettings> settings)
            : base(repo)
        {
            _repo = repo;
            _AppSettings = settings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QuestionDetails(string serviceId)
        {
            var questions = TheRepository.GetServiceProvidersByService(Convert.ToInt32(serviceId));
            //questions = _db.TradeAdditionalQuestions.Where(a => a.TradeId == Convert.ToInt32(tradeId)).ToList();
            return PartialView("_QuestionDetailsModal", questions);
        }
    }
}