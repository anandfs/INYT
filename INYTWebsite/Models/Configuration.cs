using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Configuration
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
    }
}
