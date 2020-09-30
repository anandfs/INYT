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
    public class CustomerSignupController : BaseController
    {
        Repository _repo = null;
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public CustomerSignupController(Repository repo, IOptions<AppSettings> settings,
            IEmailManager emailManager)
            : base(repo)
        {
            _repo = repo;
            _emailManager = emailManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("CustSignup")]
        public IActionResult CustSignup()
        {
            CustomerModel model = new CustomerModel();

            List<ServiceModel> services = TheRepository.GetServices().ToList();

            var servicesList = services.Select(x => new SelectListItem
            {
                Text = x.Service.ToString(),
                Value = x.id.ToString()
            }).ToList();

            ViewData["Services"] = servicesList;
            return View(model);
        }

        [Route("CustLogin")]
        public IActionResult CustLogin()
        {
            return View();
        }

        [Route("CustLoginProcess")]
        public IActionResult CustLoginProcess(LoginModel login, string returnUrl = "")
        {
            if (IsValid(login) != null)
            {
                CustomerModel model = new CustomerModel();
                model = TheRepository.GetCustomerByEmail(login.userName);

                //if user is inactive or locked out, do not proceed any further
                if (Convert.ToBoolean(model.isActive) == false)
                {
                    ModelState.AddModelError("", "Your account is currently inactive. Please " +
                        "contact admin@inyt.com to activate yor account");
                    return View("Login");
                }

                //If valid, then set the session values
                SetSessionValuesAsync(model);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                if (Request.Form["frompage"].ToString() != null)
                {
                    string spid = Request.Form["id"].ToString();
                    string custpostcode = Request.Form["postcode"].ToString();
                    return RedirectToAction("Index", "BookService", new { id = spid, postcode = custpostcode, customerid = model.id.ToString() });
                }

                return RedirectToAction("CustIndex", "Customer");
            }

            return View("Login");
        }

        [Route("CustSignupProcess")]
        [HttpPost]
        public IActionResult CustSignupProcess(CustomerModel model)
        {
            model.isActive = true; // should be after user has validated their email. but temp for now
            var customer = TheRepository.CreateCustomer(model);

            model.id = customer.id;
            model.isEmailVerified = false;
            model.isRegistrationApproved = false;
            model.createdDate = DateTime.Now;
            model.password = PasswordHash.PasswordHash.CreateHash(model.password).Replace("1000:", String.Empty);

            LoginModel login = new LoginModel
            {
                userName = model.emailAddress,
                password = model.password,
                userid = customer.id,
                createdDate = DateTime.Now
            };

            var createdLogin = TheRepository.CreateLogin(login);

            //On successful creation of record, send an authorisation email to the servieprovider
            EmailInfo email_sp = new EmailInfo
            {
                emailType = "Customer_AuthorisationEmail",
                FromAddress = "donotreply@inyt.com",
                ToAddress = model.emailAddress,
                IsBodyHtml = true,
                Subject = "Welcome from I NEED YOUR TIME"
            };

            _emailManager.SendEmail(email_sp);

            //Also send an email to the Administrator with the tradesperson details
            EmailInfo email_admin = new EmailInfo
            {
                emailType = "Admin_NewServiceProvider",
                FromAddress = "donotreply@inyt.com",
                ToAddress = "anand@futuresolutionsltd.com",
                IsBodyHtml = true,
                Subject = "New customer on INYT website"
            };

            _emailManager.SendEmail(email_admin);

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

        private async void SetSessionValuesAsync(CustomerModel model)
        {

            var claims = new List<Claim>
            {
                new Claim("UserId", model.id.ToString(), ClaimValueTypes.Integer),
                new Claim("FirstName", model.firstName.ToString().Trim(), ClaimValueTypes.String),
                new Claim("LastName", model.lastName.ToString().Trim(), ClaimValueTypes.String),
                new Claim("FullName", string.Format("{0} {1}", model.firstName.Trim(), model.lastName.Trim()), ClaimValueTypes.String),
                new Claim("Email", model.emailAddress.ToString().Trim(), ClaimValueTypes.String),
                new Claim("Postcode", model.postcode.ToString().Trim(), ClaimValueTypes.String),
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