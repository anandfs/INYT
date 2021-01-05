using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class BookingModel
    {
        public SelectList servicesList { get; set; }
        public string postCode { get; set; }
        public string selectedService { get; set; }

        public int id { get; set; }

        public CustomerModel customer { get; set; }
        public ServiceProviderModel serviceProvider { get; set; }
        public List<ServiceProviderAdditionalQuestionsModel> questionsList { get; set; }

        public List<AdditionalQuestionsModel> additionalQuestionsList { get; set; }

        public int serviceId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}")]
        public DateTime bookingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh:MM}")]

        public DateTime bookingTime { get; set; }
        public double bookingAmount { get; set; }
        public string bookingPaymentType { get; set; }

        public bool bookingAccepted { get; set; }
        public bool bookingFulfilled { get; set; }

        public int customerId { get; set; }

        public int serviceProviderId { get; set; }
        public List<AvailabilityDatesModel> availabilityDates { get; set; }

        public List<CalendarDates> calendar { get; set; }
        public string serviceName { get; set; }
        public string bookingReference { get; set; }
    }

    public class AdditionalQuestionsModel
    {
        public int id { get; set; }
        public int serviceId { get; set; }
        public int bookingId { get; set; }
        public int customerId { get; set; }
        public string additionalQuestion { get; set; }
        public string answer { get; set; }
    }

    public class BookingsListModel
    {
        public List<BookingModel> bookings { get; set; }
        public ServiceProviderModel serviceProvider { get; set; }
        public CustomerModel customer { get; set; }
    }

    public class CustomerAddress
    {
        public string housenumber { get; set; }
        public string streetname { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
    }
}
