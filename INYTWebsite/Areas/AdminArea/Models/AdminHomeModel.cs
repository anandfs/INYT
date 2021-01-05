using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Areas.AdminArea.Models
{
    public class AdminHomeModel
    {
        public int customercount { get; set; }
        public int serviceprovidercount { get; set; }
        public int bookingscount { get; set; }
        public double bookingsamount { get; set; }
    }
}
