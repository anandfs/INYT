using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class ServiceProvider
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int? TradeId { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string CompanySize { get; set; }
        public bool? IsVatRegistered { get; set; }
        public string Vatnumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public string DeactivationReason { get; set; }
        public string Website { get; set; }
        public string Picture { get; set; }
        public int? MembershipId { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string VerifyCode { get; set; }
    }
}
