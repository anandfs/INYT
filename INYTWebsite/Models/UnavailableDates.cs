using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class UnavailableDates
    {
        public int Id { get; set; }
        public DateTime? UnavailableDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
