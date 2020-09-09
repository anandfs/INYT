using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class CustomerModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public string contactNumber { get; set; }
        public List<BookingModel> bookings { get; set; }
        public bool isActive { get; set; }
        public bool isEmailVerified { get; set; }
        public bool isRegistrationApproved { get; set; }
        public DateTime createdDate { get; set; }
        public string password { get; set; }
        public string repeatPassword { get; set; }
        public List<InvoiceModel> invoices { get; set; }
        public bool hasAgreedTC { get; set; }
    }
}
