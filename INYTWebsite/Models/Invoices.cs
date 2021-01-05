using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? ServiceProviderId { get; set; }
        public string PaypalBookingReference { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
