using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Models
{
    public class AvailabilityDates
    {
        public Guid ID { get; set; }
        public DateTime Dates { get; set;  }
        public string WeekName { get; set; }
        public int ServiceProviderId { get; set; }
        public string Availability { get; set; }
    }
}
