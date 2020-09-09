using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Invoices
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? ServiceProviderId { get; set; }
        public int? BookingId { get; set; }
        public int? InvoiceNumber { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}
