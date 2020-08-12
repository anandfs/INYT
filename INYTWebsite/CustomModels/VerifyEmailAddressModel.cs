using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class VerifyEmailAddressModel
    {
        public bool validFormat { get; set; }
        public bool free { get; set; }
        public bool deliverable { get; set; }
    }

    public class VerifyPhoneNumberModel
    {
        public bool valid { get; set; }
        public string international_format { get; set; }
    }
}
