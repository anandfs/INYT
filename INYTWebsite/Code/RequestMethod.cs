using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class RequestMethod
    {
        private string configUserName = string.Empty;
        private string configPassword = string.Empty;
        private string restService = string.Empty;

        public HttpStatusCode statusCode { get; set; }

        public HttpResponseMessage GetContent(string url)
        {
            string RestUrl = string.Format("{0}{1}", restService, url);
            try
            {
                var client = new HttpClient();
                HttpResponseMessage _responseContent = client.GetAsync(RestUrl).Result;
                client.Dispose();
                return _responseContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
