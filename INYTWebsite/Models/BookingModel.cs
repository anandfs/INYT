using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public List<TradeAdditionalQuestions> questionsList { get; set; }

        public int tradeId { get; set; }
        public DateTime bookingDate { get; set; }
        public DateTime bookingTime { get; set; }
        public double bookingAmount { get; set; }
        public string bookingPaymentType { get; set; }
        public bool bookingFulfilled { get; set; }
    }
}
