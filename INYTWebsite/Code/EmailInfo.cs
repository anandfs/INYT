using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class EmailInfo
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string fileNameToAttach { get; set; }
        public string calendarToAttach { get; set; }
        public string FromDisplayName { get; set; }
        public string FromDisplayFirstName { get; set; }
        public string FromDisplayLastName { get; set; }
        public string FromDisplayCompanyName { get; set; }
        public string FromDisplayJobPosition { get; set; }
        public string emailType { get; set; }
        public string emailReplyLink { get; set; }
    }
}
