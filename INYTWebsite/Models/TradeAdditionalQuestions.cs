using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class TradeAdditionalQuestions
    {
        public int Id { get; set; }
        public int? TradeId { get; set; }
        public string AdditionalQuestion { get; set; }
        public string AnswerOptions { get; set; }
        public string AnswerOptionType { get; set; }
    }
}
