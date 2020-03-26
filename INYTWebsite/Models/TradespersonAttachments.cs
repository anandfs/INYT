using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class TradespersonAttachments
    {
        public int Id { get; set; }
        public int? TradeId { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentFileDescription { get; set; }
    }
}
