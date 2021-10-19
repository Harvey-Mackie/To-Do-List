using System;
using System.Collections.Generic;
using To_Do_List_Library.Core.Domain.Common;

namespace To_Do_List_Library.Core.Domain.Entities
{
    public class ToDoList : AuditableEntry
    {
        public Guid ToDoListId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
        public Guid UserId { get; set; }
    }
}
