using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class AvailabilitySlots
    {
        public int Id { get; set; }
        public int? ServiceProviderId { get; set; }
        public int? MinimumHours { get; set; }
        public decimal? MinimumRate { get; set; }
        public decimal? RateForAdditionalHour { get; set; }
        public int? BreakTimeInMins { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public int? MaxBookingsPerDay { get; set; }
    }
}
