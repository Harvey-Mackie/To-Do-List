using System;
using To_Do_List_Library.Core.Common;

namespace To_Do_List_Library.Core.Entities
{
    public class User : AuditableEntry
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
