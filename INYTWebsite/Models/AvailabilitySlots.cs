using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class AvailabilitySlots
    {
        public int Id { get; set; }
        public int? ServiceProviderId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public int? MaxBookingsPerDay { get; set; }
    }
}
