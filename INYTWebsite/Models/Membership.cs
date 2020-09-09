using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Membership
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? BasicSubscriptionFee { get; set; }
        public double? Commission { get; set; }
    }
}
