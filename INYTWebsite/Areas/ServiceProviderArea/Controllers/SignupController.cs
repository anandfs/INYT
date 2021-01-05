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
using RestSharp;
using RestSharp.Authenticators;


namespace INYTWebsite.Areas.ServiceProviderArea.Controllers
{
    [Area("ServiceProviderArea")]
    public class SignupController : BaseController
    {
        Repository _repo = null;
        private readonly IEmailManager _emailManager;

        /// <summary>
        /// Constructor for controller
        /// </summary>
        /// <param name="repo"></param>
        public SignupController(Repository repo, IOptions<AppSettings> settings, 
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

            List<MembershipModel> memberships = TheRepository.GetMemberships().ToList();

            var membershipList = memberships.Select(x => new SelectListItem
            {
                Text = String.Format("{0} - £{1} pm (+ {2}% commission on sales)", x.name.ToString(), 
                    x.basicSubscriptionFee.ToString(), x.commission),
                Value = x.id.ToString()
            }).ToList();

            ViewData["Services"] = servicesList;
            ViewData["Memberships"] = membershipList;

            model.memberships = membershipList;
            model.services = servicesList;

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
        public IActionResult SignupProcess(ServiceProviderModel model)
        {
            model.isActive = true; // should be after user has validated their email. but temp for now
            var serviceprovider = TheRepository.CreateServiceProvider(model);

            model.id = serviceprovider.id;
            model.emailConfirmed = false;
            model.isRegistrationApproved = false;
            model.createdDate = DateTime.Now;
            model.password = PasswordHash.PasswordHash.CreateHash(model.password).Replace("1000:", String.Empty);

            LoginModel login = new LoginModel
            {
                userName = model.emailAddress,
                password = model.password,
                userid = serviceprovider.id,
                createdDate = DateTime.Now
            };

            var createdLogin = TheRepository.CreateLogin(login);

            //Send an authorisation email to the service provider
            //SendSimpleMessage("Welcome from I NEED YOUR TIME", "anand@futuresolutionsltd.com").Content.ToString();

            //Send an email to the Administrator with the service provider details
            //SendSimpleMessage("New customer on INYT website", "anandkuppa@gmail.com").Content.ToString();

            return View(model);
        }

        [Route("changepassword")]
        public IActionResult Changepassword()
        {
            return View();
        }

        [Route("confirmemail")]
        public IActionResult ConfirmEmail(string id)
        {
            var decryptedid = EncryptionUtility.Decrypt(id.ToString());
            ServiceProviderModel model = new ServiceProviderModel();
            model = TheRepository.GetServiceProvider(Convert.ToInt32(decryptedid));
            model.emailConfirmed = true;
            TheRepository.UpdateServiceProvider(model, model);
            return View(model);
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

        //private IRestResponse SendSimpleMessage(string content, string toaddress)
        //{
        //    RestClient client = new RestClient();
        //    client.BaseUrl = new Uri("https://api.mailgun.net/v3");
        //    client.Authenticator = new HttpBasicAuthenticator("api", "key-e9cc99f3383397d70dc90da27c7d138b");
        //    RestRequest request = new RestRequest();
        //    request.AddParameter("domain", "sandboxaf49e6982a794654beb9ad6e8e82acf6.mailgun.org", ParameterType.UrlSegment);
        //    request.Resource = "sandboxaf49e6982a794654beb9ad6e8e82acf6.mailgun.org/messages";
        //    request.AddParameter("from", "Welcome from INYT <hello@mailgun.org>");
        //    request.AddParameter("to", toaddress);
        //    request.AddParameter("to", "YOU@YOUR_DOMAIN_NAME");
        //    request.AddParameter("subject", "Hello");
        //    request.AddParameter("text", content);
        //    request.Method = Method.POST;
        //    return client.Execute(request);
        //}
    }
}