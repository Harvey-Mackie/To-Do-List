using System;
using To_Do_List_Library.Core.Common;

namespace To_Do_List_Library.Core.Entities
{
    public class ToDoItem : AuditableEntry
    {
        public Guid ToDoItemId { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; } 
        public Guid ToDoListId { get; set; }
    }
}
