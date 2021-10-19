using System;

namespace To_Do_List_Library.Core.Domain.Common
{
    public class AuditableEntry
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
