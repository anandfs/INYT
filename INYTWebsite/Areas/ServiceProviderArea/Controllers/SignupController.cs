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


namespace INYTWebsite.Areas.ServiceProviderArea.Controllers
{
    [Area("ServiceProviderArea")]
    public class SignupController : BaseController
    {
        Repository _repo = null;
        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public SignupController(Repository repo, IOptions<AppSettings> settings, IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Signup")]
        public IActionResult Signup()
        {
            ServiceProviderModel model = new ServiceProviderModel();

            List<ServiceModel> services = TheRepository.GetServices().ToList();

            var servicesList = services.Select(x => new SelectListItem
            {
                Text = x.Service.ToString(),
                Value = x.id.ToString()
            }).ToList();

            ViewData["Services"] = servicesList;
            return View(model);
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("LoginProcess")]
        public IActionResult LoginProcess(LoginModel login, string returnUrl = "")
        {
            if (IsValid(login) != null)
            {
                ServiceProviderModel model = new ServiceProviderModel();
                model = TheRepository.GetServiceProviderByEmail(login.userName);

                ServiceModel services = new ServiceModel();
                services = TheRepository.GetServiceById(model.serviceId);
                model.service = services.Service;

                //if user is inactive or locked out, do not proceed any further
                if (Convert.ToBoolean(model.isActive) == false)
                {
                    ModelState.AddModelError("", "Your account is currently inactive. Please " +
                        "contact admin@inyt.com to activate yor account");
                    return View(model);
                }

                //If valid, then set the session values
                SetSessionValuesAsync(model);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index","ServiceProvider");
            }

            return View("Login");
        }

        [Route("SignupProcess")]
        [HttpPost]
        public IActionResult SignupProcess(ServiceProviderModel model)
        {

            var serviceprovider = TheRepository.CreateServiceProvider(model);

            model.id = serviceprovider.id;
            model.isEmailVerified = false;
            model.isRegistrationApproved = false;
            model.createdDate = DateTime.Now;
            model.password = PasswordHash.PasswordHash.CreateHash(model.password).Replace("1000:", String.Empty);

            LoginModel login = new LoginModel
            {
                userName = model.emailAddress,
                password = model.password,
                createdDate = DateTime.Now
            };

            var createdLogin = TheRepository.CreateLogin(login);

            //On successful creation of record, send an authorisation email to the tradesperson
            //EmailInfo email_sp = new EmailInfo
            //{
            //    emailType = "ServiceProvider_AuthorisationEmail",
            //    FromAddress = "donotreply@inyt.com",
            //    ToAddress = model.emailAddress,
            //    IsBodyHtml = true,
            //    Subject = "Welcome from I NEED YOUR TIME"
            //};

            //_emailManager.SendEmail(email_sp);

            ////Also send an email to the Administrator with the tradesperson details
            //EmailInfo email_admin = new EmailInfo
            //{
            //    emailType = "Admin_NewServiceProvider",
            //    FromAddress = "donotreply@inyt.com",
            //    ToAddress = "anand@futuresolutionsltd.com",
            //    IsBodyHtml = true,
            //    Subject = "New service provider on INYT website"
            //};

            //_emailManager.SendEmail(email_admin);

            return View(model);
        }

        [Route("changepassword")]
        public IActionResult Changepassword()
        {
            return View();
        }



        private LoginModel IsValid(LoginModel user)
        {
            LoginModel validatedUser = TheRepository.GetLoginByUsername(user.userName);

            if (validatedUser != null)
            {
                String passwordFromDb = String.Format("1000:{0}", validatedUser.password.ToString());

                if (PasswordHash.PasswordHash.ValidatePassword(user.password, passwordFromDb))
                {
                    return validatedUser;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

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
                new Claim("Trade", model.service.ToString().Trim(), ClaimValueTypes.String),
                new Claim("ContactNumber", model.contactNumber.ToString().Trim(), ClaimValueTypes.String),
            };

            var userIdentity = new ClaimsIdentity(claims, "UserCookieScheme");
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            HttpContext.Session.SetString("LogUserString", string.Format("[{0}] {1} {2} <{3}>",
                model.id, model.firstName, model.lastName, model.emailAddress));

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