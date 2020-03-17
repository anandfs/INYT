using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class AuditLog
    {
        public Guid AuditLogId { get; set; }
        public int? UserId { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventType { get; set; }
        public string EntityName { get; set; }
        public string RecordId { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
