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

    }
}
