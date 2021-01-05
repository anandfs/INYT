using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Models
{
    public class ConfigurationModel
    {
        public int id { get; set; }
        public string provider { get; set; }
        public string keyName { get; set; }
        public string keyValue { get; set; }

    }
}
