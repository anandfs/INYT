using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite
{
    public class AppSettings
    {
        public PostcodesAPI PostcodesAPI { get; set; }

        public SendGridAPI SendGridAPI { get; set; }

        public GoogleAPI GoogleAPI { get; set; }

        public PayPalAPI PayPalAPI { get; set; }

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

    public class GoogleAPI
    {
        public string distanceAPI { get; set; }
        public string placesAPI { get; set; }
    }

    public class PayPalAPI
    {
        public string PAYPAL_SANDBOX_CLIENT { get; set; }
        public string PAYPAL_SANDBOX_SECRET { get; set; }
    }
}
