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

namespace INYTWebsite.Areas.CustomerArea.Controllers
{
    [Area("CustomerArea")]
    public class CustomerController : BaseController
    {
        Repository _repo = null;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public CustomerController(Repository repo, IOptions<AppSettings> settings, IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
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
                }

                return View(model);
            }

            return View();
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

    }
}