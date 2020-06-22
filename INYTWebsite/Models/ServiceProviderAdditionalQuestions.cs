using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class ServiceProviderAdditionalQuestions
    {
        public int Id { get; set; }
        public int? ServiceProviderId { get; set; }
        public string AdditionalQuestion { get; set; }
        public string AnswerOptions { get; set; }
        public string AnswerOptionType { get; set; }
    }
}
