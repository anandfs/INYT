using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INYTWebsite.Model
{
    public partial class Ratings
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? JobId { get; set; }
        [Column("Ratings")]
        public int? Ratings1 { get; set; }
        [StringLength(255)]
        public string RatingComments { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public bool? Display { get; set; }
    }
}
