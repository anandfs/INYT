using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INYTWebsite.Model
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? MessageFromId { get; set; }
        public int? MessageToId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MessageDate { get; set; }
        [StringLength(2000)]
        public string MessageBody { get; set; }
        [StringLength(100)]
        public string MessageSubject { get; set; }
        [StringLength(100)]
        public string MessageAttachment { get; set; }
        public bool? IsRead { get; set; }
        public int? ParentId { get; set; }
        [Column("isArchived")]
        public bool? IsArchived { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
