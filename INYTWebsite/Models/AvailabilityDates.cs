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
        public int minHours { get; set;  }
        public decimal minRate { get; set; }
        public int breakTimeInMins { get; set; }
        public decimal additionalRate { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

    }
}
