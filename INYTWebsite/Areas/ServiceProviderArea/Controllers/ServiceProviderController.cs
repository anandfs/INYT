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

        [Route("index/{id}")]
        public IActionResult Index(int id)
        {
            ServiceProviderModel model = TheRepository.GetServiceProvider(id);

            if (model != null)
            {
                model.bookings = TheRepository.GetAllBookings(id);
            }

            return View(model);
        }

        [Route("schedules")]
        //public IActionResult Schedules(int id)
        //{
        //    ServiceProviderModel model = TheRepository.GetServiceProvider(id);

        //    if (model != null)
        //    {
        //        model.slots = TheRepository.GetAvailabilitySlots(id);
        //    }

        //    return View(model);
        //}

        [Route("invoices")]
        public IActionResult Invoices()
        {
            return View();
        }

        [Route("ratings")]
        public IActionResult Ratings()
        {
            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("changepassword")]
        public IActionResult Changepassword()
        {
            return View();
        }

        [Route("Signup")]
        //public IActionResult Signup()
        //{
        //    ServiceProviderModel model = new ServiceProviderModel();

        //    List<TradeModel> trades = TheRepository.GetTrades().ToList();

        //    var tradesList = trades.Select(x => new SelectListItem
        //    {
        //        Text = x.Trade.ToString(),
        //        Value = x.Id.ToString()
        //    }).ToList();

        //    ViewData["Services"] = tradesList;
        //    return View(model);
        //}

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("LoginProcess")]
        //public IActionResult LoginProcess(LoginModel login, string returnUrl = "")
        //{
        //    if (IsValid(login) != null)
        //    {
        //        ServiceProviderModel model = new ServiceProviderModel();
        //        model = TheRepository.GetServiceProvider() _modelFactory.Create(tradesperson);

        //        TradeMaster tradeMaster = new TradeMaster();
        //        tradeMaster = _db.TradeMaster.Find(model.tradeId);

        //        model.trade = tradeMaster.Trade;

        //        //if user is inactive or locked out, do not proceed any further
        //        if (Convert.ToBoolean(model.isActive) == false)
        //        {
        //            ModelState.AddModelError("", "Your account is currently inactive. Please contact admin@inyt.com to activate yor account");
        //            return View(model);
        //        }

        //        //If valid, then set the session values
        //        SetSessionValuesAsync(model);

        //        if (!string.IsNullOrEmpty(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }

        //        return RedirectToAction("Index", new { model.id });
        //    }

        //    return View("Login");
        //}

        [Route("SignupProcess")]
        [HttpPost]
        //public IActionResult SignupProcess(ServiceProviderModel model)
        //{
        //    Tradesperson tradesperson = new Tradesperson
        //    {
        //        AddressLine1 = model.addressLine1,
        //        AddressLine2 = model.addressLine2,
        //        City = model.city,
        //        CompanyName = model.companyName,
        //        CompanyNumber = model.companyNumber,
        //        CompanySize = model.companySize,
        //        ContactNumber = model.contactNumber,
        //        Country = model.country,
        //        EmailAddress = model.emailAddress,
        //        FirstName = model.firstName,
        //        IsActive = true,
        //        LastName = model.lastName,
        //        Postcode = model.postcode,
        //        Region = model.region,
        //        TradeId = model.tradeId
        //    };

        //    _db.Tradesperson.Add(tradesperson);
        //    _db.SaveChanges();

        //    model.id = tradesperson.Id;

        //    model.isEmailVerified = false;
        //    model.isRegistrationApproved = false;
        //    model.createdDate = DateTime.Now;

        //    model.password = PasswordHash.PasswordHash.CreateHash(model.password).Replace("1000:", String.Empty);

        //    Login login = new Login
        //    {
        //        UserId = model.id,
        //        Username = model.emailAddress,
        //        Password = model.password,
        //        CreatedDate = DateTime.Now
        //    };

        //    _db.Login.Add(login);
        //    _db.SaveChanges();

        //    //On successful creation of record, send an authorisation email to the tradesperson
        //    //EmailInfo email_sp = new EmailInfo
        //    //{
        //    //    emailType = "ServiceProvider_AuthorisationEmail",
        //    //    FromAddress = "donotreply@inyt.com",
        //    //    ToAddress = model.emailAddress,
        //    //    IsBodyHtml = true,
        //    //    Subject = "Welcome from I NEED YOUR TIME"
        //    //};

        //    //_emailManager.SendEmail(email_sp);

        //    ////Also send an email to the Administrator with the tradesperson details
        //    //EmailInfo email_admin = new EmailInfo
        //    //{
        //    //    emailType = "Admin_NewServiceProvider",
        //    //    FromAddress = "donotreply@inyt.com",
        //    //    ToAddress = "anand@futuresolutionsltd.com",
        //    //    IsBodyHtml = true,
        //    //    Subject = "New service provider on INYT website"
        //    //};

        //    //_emailManager.SendEmail(email_admin);

        //    return View(model);
        //}

        //private LoginModel IsValid(LoginModel user)
        //{
        //     Login validatedUser = _db.Login.Where(a => a.Username == user.userName).FirstOrDefault();

        //    if (validatedUser != null)
        //    {
        //        String passwordFromDb = String.Format("1000:{0}", validatedUser.Password);

        //        if (PasswordHash.PasswordHash.ValidatePassword(user.password, passwordFromDb))
        //        {
        //            LoginModel loginModel = new LoginModel
        //            {
        //                userName = validatedUser.Username,
        //                password = validatedUser.Password,
        //                id = validatedUser.Id
        //            };

        //            return loginModel;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    return null;
        //}

        private async void SetSessionValuesAsync(ServiceProviderModel model)
        {

            var claims = new List<Claim>
            {
                new Claim("UserId", model.id.ToString(), ClaimValueTypes.Integer),
                new Claim("FirstName", model.firstName.ToString().Trim(), ClaimValueTypes.String),
                new Claim("LastName", model.lastName.ToString().Trim(), ClaimValueTypes.String),
                new Claim("FullName", string.Format("{0} {1}", model.firstName.Trim(), model.lastName.Trim()), ClaimValueTypes.String),
                new Claim("Email", model.emailAddress.ToString().Trim(), ClaimValueTypes.String),
                new Claim("CompanyName", model.companyName.ToString().Trim(), ClaimValueTypes.String),
                new Claim("CompanyPostcode", model.postcode.ToString().Trim(), ClaimValueTypes.String),
                new Claim("Trade", model.trade.ToString().Trim(), ClaimValueTypes.String),
                new Claim("ContactNumber", model.contactNumber.ToString().Trim(), ClaimValueTypes.String),
            };

            var userIdentity = new ClaimsIdentity(claims, "UserCookieScheme");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //HttpContext.Session.SetString("LogUserString", string.Format("[{0}] {1} {2} <{3}>", model.id, model.firstName, model.lastName, model.emailAddress));

            await HttpContext.SignInAsync("UserCookieScheme", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                    IsPersistent = false,
                }
            );
        }
       
    }
}