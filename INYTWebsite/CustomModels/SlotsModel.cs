using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class SlotsModel
    {
        public int id { get; set; }
        public int serviceproviderId { get; set; }
        public string serviceproviderName { get; set; }
        public int minimumHours { get; set; }
        public decimal minimumRate { get; set; }
        public decimal? rateForAdditionalHour { get; set; }
        public int breakTimeInMins { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string dayOfWeek { get; set; }
        public int maxBookingsPerDay { get; set; }

        public IEnumerable<SelectListItem> minHoursList { get; set; }
        public IEnumerable<SelectListItem> startTimeList { get; set; }
        public IEnumerable<SelectListItem> endTimeList { get; set; }


    }
}
