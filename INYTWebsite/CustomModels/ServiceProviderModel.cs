using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class ServiceProviderModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public int serviceId { get; set; }
        public string service { get; set; }
        public string description { get; set; }
        public string companyName { get; set; }
        public string companyNumber { get; set; }
        public string companySize { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
        public string contactNumber { get; set; }
        public bool isActive { get; set; }
        public string deactivationReason { get; set; }
        public string website { get; set; }

        public string distanceinmiles { get; set; }
        
        public string password { get; set; }
        public string repeatPassword { get; set; }
        public int rating { get; set; }
        public bool isEmailVerified { get; set; }
        public bool isRegistrationApproved { get; set; }
        public DateTime createdDate { get; set; }
        public List<SlotsModel> slots { get; set; }
        public List<AdditionalQuestionsModel> questionsList { get; set; }
        public List<BookingModel> bookings { get; set; }
        public string selectedService { get; set; }
        public int totalCustomers { get; set; }
        public int totalCustomersToday { get; set; }
        public int totalNewBookings { get; set; }

        public SlotsModel newslot { get; set; }
        public List<ServiceProviderAdditionalQuestionsModel> additionalQuestions { get; set; }
    }
}
