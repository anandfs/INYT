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
using System.IO;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Mail;
using System.Net;
using INYTWebsite.Controllers;

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

                if (!String.IsNullOrEmpty(Request.Form["frompage"].ToString()))
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
            model.VerifyCode = Guid.NewGuid().ToString();

            var exist = TheRepository.CheckEmailExist(model.emailAddress);

            if(exist)
            {
                TempData["exist"] = "Email Exists";
                return View("CustSignup", model);
            }
            var customer = TheRepository.CreateCustomer(model);

            model.id = customer.id;
            model.emailConfirmed = false;
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

            var encryptedid = EncryptionUtility.Encrypt(createdLogin.id.ToString());

            //Send an authorisation email to the customer
            EmailInfo emailInfo = new EmailInfo();
            var callbackUrl = Url.Action("VerifyEmail", "CustomerSignup", new { userId = customer.id, code = model.VerifyCode }, Request.Scheme, Request.Host.Value + "/CustomerArea");
            emailInfo.Body = $"Welcome from I NEED YOUR TIME. Click <a href='{callbackUrl}'>here</a> to confirm your email";
            emailInfo.emailType = "WelcomeEmail";
            emailInfo.IsBodyHtml = true;
            emailInfo.Subject = "Welcome to INYT";
            emailInfo.ToAddress = model.emailAddress;
            //_emailManager.SendEmail(emailInfo);
            var model2 = new Emailmodel
            {
                Name = model.firstName,
                Body = emailInfo.Body
            };
            var renderedHTML = ControllerExtensions.RenderViewAsHTMLString(this,"_VerifyEmail.cshtml", model2);

            EmailManager.SendEmail2(model.emailAddress,"VerifyAccount",renderedHTML.Result);
            //SendSimpleMessage(String.Format("<strong>Welcome from I NEED YOUR TIME. Click <a href='CustomerSignup/ConfirmEmail/{0}'>here</a> to confirm your email", "anand@futuresolutionsltd.com"), encryptedid).Content.ToString();

            //Send an email to the Administrator with the customer details
            //EmailInfo emailInfo = new EmailInfo();
            emailInfo.Body = "New customer on INYT website";
            emailInfo.emailType = "NewCustomerEmail";
            emailInfo.IsBodyHtml = true;
            emailInfo.Subject = "New customer";
            emailInfo.ToAddress = "anandkuppa@gmail.com";
            _emailManager.SendEmail(emailInfo);

            //SendSimpleMessage("New customer on INYT website", "anandkuppa@gmail.com").Content.ToString();

            return View(model);
        }
       
        [Route("changepassword")]
        public IActionResult Changepassword()
        {
            return View();
        }
        public IActionResult VerifyEmail(int userId,string code)
        {
            var cus = TheRepository.VerifyAccount(userId, code);
            int result = 0;
            
            if(cus!=null)
            {
                result = 1;
                string body = "Your account has been verified now you can login successfully.";
                var model2 = new Emailmodel
                {
                    Name = cus.FirstName,
                    Body = body
                };
                var renderedHTML = ControllerExtensions.RenderViewAsHTMLString(this, "_VerifyEmail.cshtml", model2);

                EmailManager.SendEmail2(cus.EmailAddress, "Account Verified", renderedHTML.Result);
            }
            else
            {
                result = 0;
            }
            ViewBag.result = result;
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