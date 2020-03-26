using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class TradespersonBooking
    {
        public int Id { get; set; }
        public int? TradepersonId { get; set; }
        public string BookingDescription { get; set; }
        public bool? AvailableForBooking { get; set; }
        public string FreeOrPaidBooking { get; set; }
        public decimal? MinimumAmount { get; set; }
        public int? MinimumTimeInMinutes { get; set; }
    }
}
