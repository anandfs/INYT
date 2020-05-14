using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INYTWebsite.Code
{
    public class MessageCenterTemplate
    {
        public string firstName { get; internal set; }
        public string lastName { get; internal set; }
        public string companyName { get; internal set; }
        public string job_position { get; internal set; }
        public int message_count { get; internal set; }
        public string message_text { get; internal set; }
        public string reply_link { get; internal set; }
        public string subject { get; internal set; }
    }
}
