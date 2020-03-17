using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? MessageFromId { get; set; }
        public int? MessageToId { get; set; }
        public DateTime? MessageDate { get; set; }
        public string MessageBody { get; set; }
        public string MessageSubject { get; set; }
        public string MessageAttachment { get; set; }
        public bool? IsRead { get; set; }
        public int? ParentId { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
