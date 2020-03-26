using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class AdditionalQuestionAnswers
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? BookingId { get; set; }
        public int? TradeId { get; set; }
        public string AdditionalQuestion { get; set; }
        public string Answer { get; set; }
    }
}
