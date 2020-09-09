using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Models
{
    public class LoginModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int userid { get; set; }
        public DateTime createdDate { get; set; }
    }
}
