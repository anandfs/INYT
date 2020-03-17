using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class LookupTable
    {
        public int Id { get; set; }
        public string LookupType { get; set; }
        public string LookupValue { get; set; }
    }
}
