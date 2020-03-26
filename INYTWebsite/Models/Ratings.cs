using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Ratings
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? BookingId { get; set; }
        public int? Ratings1 { get; set; }
        public string RatingComments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Display { get; set; }
    }
}
