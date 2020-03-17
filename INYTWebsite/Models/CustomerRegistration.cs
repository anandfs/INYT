using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class CustomerRegistration
    {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerMobileNumber { get; set; }
        public bool? HasAgreedTc { get; set; }
    }
}
