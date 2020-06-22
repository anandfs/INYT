using System;
using System.Collections.Generic;

namespace INYTWebsite.Models
{
    public partial class ServiceProviderAttachments
    {
        public int Id { get; set; }
        public int? ServiceProviderId { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentFileDescription { get; set; }
    }
}
