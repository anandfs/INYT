using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Models
{
    public class BookingModel
    {
        public SelectList tradesList { get; set; }
        public string postCode { get; set; }
        public string selectedTrade { get; set; }

        public int id { get; set; }

        public CustomerModel customer { get; set; }
        public Tradesperson serviceProvider { get; set; }
        public List<AdditionalQuestionsModel> questionsList { get; set; }

        public int tradeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}")]
        public DateTime bookingDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh:MM}")]

        public DateTime bookingTime { get; set; }
        public double bookingAmount { get; set; }
        public string bookingPaymentType { get; set; }
        public bool bookingFulfilled { get; set; }

        public int serviceProviderId { get; set; }
    }

    public class AdditionalQuestionsModel
    {
        public int questionId { get; set; }
        public int tradeId { get; set; }
        public string additionalQuestion { get; set; }
        public string answerOptions { get; set; }
        public string answeroptionType { get; set; }
        public string answer { get; set; }
    }
}
