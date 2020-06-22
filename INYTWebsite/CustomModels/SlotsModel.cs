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
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string dayOfWeek { get; set; }
        public int maxBookingsPerDay { get; set; }
    }
}
