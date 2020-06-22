using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class ServiceProviderBooking
    {
        public int Id { get; set; }
        public int? ServiceProviderId { get; set; }
        public string BookingDescription { get; set; }
        public bool? AvailableForBooking { get; set; }
        public string FreeOrPaidBooking { get; set; }
        public decimal? MinimumAmount { get; set; }
        public int? MinimumTimeInMinutes { get; set; }
    }
}
