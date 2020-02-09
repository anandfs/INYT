using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INYTWebsite.Model
{
    public partial class Tradesperson
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string Trade { get; set; }
        public string Description { get; set; }
        [StringLength(20)]
        public string CompanyNumber { get; set; }
        [StringLength(10)]
        public string CompanySize { get; set; }
        [StringLength(50)]
        public string AddressLine1 { get; set; }
        [StringLength(50)]
        public string AddressLine2 { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string Region { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Postcode { get; set; }
        [StringLength(50)]
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(1000)]
        public string DeactivationReason { get; set; }
        [StringLength(200)]
        public string Website { get; set; }
    }
}
