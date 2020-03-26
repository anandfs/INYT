using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? TradespersonId { get; set; }
        public int? TradeId { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? BookingTime { get; set; }
        public decimal? BookingAmount { get; set; }
        public string BookingPaymentType { get; set; }
        public bool? BookingFulfilled { get; set; }
    }
}
