using INYTWebsite.CustomModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public static class ControllerExtensions
    {
        #region Get Session Values
        public static int GetCurrentUserId(this Controller controller)
        {
            var userId = controller.User.Claims.FirstOrDefault(x => x.Type == "UserId");

            if (userId != null)
                return Convert.ToInt32(userId.Value);
            else
                return 0;
        }

        public static int GetCurrentCompanyId(this Controller controller)
        {
            var companyId = controller.User.Claims.FirstOrDefault(x => x.Type == "CompanyId");

            if (companyId != null)
                return Convert.ToInt32(companyId.Value);
            else
                return 0;
        }

        public static string GetCurrentUserName(this Controller controller)
        {
            var userName = controller.User.Claims.FirstOrDefault(x => x.Type == "FullName");

            if (userName != null)
                return userName.Value;
            else
                return string.Empty;
        }

        public static string GetCurrentUserEmail(this Controller controller)
        {
            var userEmail = controller.User.Claims.FirstOrDefault(x => x.Type == "Email");

            if (userEmail != null)
                return userEmail.Value;
            else
                return string.Empty;
        }

        public static string GetCurrentUserCompanyName(this Controller controller)
        {
            var companyName = controller.User.Claims.FirstOrDefault(x => x.Type == "CompanyName");

            if (companyName != null)
                return companyName.Value;
            else
                return string.Empty;
        }
        #endregion

        #region Render Views (Generate HTML for a view based on Model)
        public static async Task<string> RenderViewAsHTMLString<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.GetView("~/", string.Format("~/PDFTemplates/{0}.cshtml", viewName), !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        #endregion

        #region Verify (Based on 3rd Party APIs)
        public static VerifyEmailAddressModel VerifyEmailAddress(this Controller controller, string email)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiKey = "AQjxgfLUQtGbjWcF98FifwiIyqlDGq9mr2prGNfRtvAi7cExsvaEeyYgdUx7M1Bw";
                var uri = string.Format("https://api.trumail.io/v2/lookups/json?email={0}", email);
                var response = httpClient.GetStringAsync(new Uri(uri)).Result;

                return JsonConvert.DeserializeObject<VerifyEmailAddressModel>(response);
            }
        }

        public static VerifyPhoneNumberModel VerifyPhoneNumber(this Controller controller, string phoneNumber)
        {
            using (var httpClient = new HttpClient())
            {
                var apiKey = "9bfa5f856928fbb3a33ec11fd6295cad";
                var uri = string.Format("http://apilayer.net/api/validate?access_key={0}&number={1}&country_code=GB&format=1", apiKey, phoneNumber);
                var response = httpClient.GetStringAsync(new Uri(uri)).Result;

                return JsonConvert.DeserializeObject<VerifyPhoneNumberModel>(response);
            }
        }
        #endregion
    }
}
