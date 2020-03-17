using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INYTWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace INYTWebsite.Controllers
{
    public class TradesController : Controller
    {
        public INYTContext _db;
        private AppSettings _AppSettings;

        public TradesController(INYTContext db, IOptions<AppSettings> settings)
        {
            _db = db;
            _AppSettings = settings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult QuestionDetails(string tradeId)
        {
            List<TradeAdditionalQuestions> questions = new List<TradeAdditionalQuestions>();
            questions = _db.TradeAdditionalQuestions.Where(a => a.TradeId == Convert.ToInt32(tradeId)).ToList();
            return PartialView("_QuestionDetailsModal", questions);
        }
    }
}