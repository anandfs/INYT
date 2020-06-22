using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.CustomModels
{
    public class ServiceProviderAdditionalQuestionsModel
    {
        public int id { get; set; }
        public int serviceProviderId { get; set; }
        public string additionalQuestion { get; set; }
        public string answerOptions { get; set; }
        public string answerOptionType { get; set; }
    }
}
