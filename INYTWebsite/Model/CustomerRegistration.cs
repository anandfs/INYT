using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INYTWebsite.Model
{
    public partial class CustomerRegistration
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string CustomerFirstName { get; set; }
        [StringLength(50)]
        public string CustomerLastName { get; set; }
        [StringLength(50)]
        public string CustomerEmailAddress { get; set; }
        [StringLength(1000)]
        public string CustomerPassword { get; set; }
        [StringLength(50)]
        public string CustomerMobileNumber { get; set; }
        [Column("HasAgreedTC")]
        public bool? HasAgreedTc { get; set; }
    }
}
