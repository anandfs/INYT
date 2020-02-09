using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INYTWebsite.Model
{
    public partial class Job
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string JobTradesperson { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobRequiredDate { get; set; }
        public string JobRequiredAddress { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(10)]
        public string JobReference { get; set; }
        [StringLength(1000)]
        public string JobCloseReason { get; set; }
        [StringLength(3)]
        public string Currency { get; set; }
    }
}
