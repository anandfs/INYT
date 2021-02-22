using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class RatingModel
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int bookingId { get; set; }
        public int ratings { get; set; }
        public string ratingComments { get; set; }
        public ServiceProviderModel serviceprovider { get; set; }
        public CustomerModel customer { get; set; }
        public BookingModel booking { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public bool display { get; set; }
    }
}
