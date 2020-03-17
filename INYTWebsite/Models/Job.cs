using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Job
    {
        public int Id { get; set; }
        public string JobTradesperson { get; set; }
        public DateTime? JobRequiredDate { get; set; }
        public string JobRequiredAddress { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string JobReference { get; set; }
        public string JobCloseReason { get; set; }
        public string Currency { get; set; }
    }
}
