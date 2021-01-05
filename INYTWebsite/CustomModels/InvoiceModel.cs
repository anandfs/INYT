using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class InvoiceModel
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int serviceProviderId { get; set; }
        public string paypalBookingReference { get; set; }
        public string invoiceNumber { get; set; }
        public double amount { get; set; }
        public DateTime paidDate { get; set; }

        public CustomerModel customer { get; set; }
        public ServiceProviderModel serviceprovider { get; set; }
        public List<BookingModel> bookings { get; set; }
    }
}
