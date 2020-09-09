using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class MembershipModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double basicSubscriptionFee { get; set; }
        public double commission { get; set; }
    }
}
