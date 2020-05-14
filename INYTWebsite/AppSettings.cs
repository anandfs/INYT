using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite
{
    public class AppSettings
    {
        public string apikey { get; set; }
        public PostcodesAPI PostcodesAPI { get; set; }

        public SendGridAPI SendGridAPI { get; set; }

        public string SendErrorMessage { get; set; }
    }

    public class SendGridAPI
    {
        public string apiKey { get; set; }
        public string clientURL { get; set; }
        public string serviceproviderauthorisationTemplate { get; set; }
    }

    public class PostcodesAPI
    {
        public string apiKey { get; set; }
    }
}
