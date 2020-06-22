using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class AvailabilityDatesModel
    {
        public Guid id { get; set; }
        public DateTime availabilityDate { get; set; }
        public string weekName { get; set; }
        public int serviceProviderId { get; set; }
        public string availability { get; set; }
    }
}
