using System;
using System.Collections.Generic;
using To_Do_List_Library.Core.Common;

namespace To_Do_List_Library.Core.Entities
{
    public class ToDoList : AuditableEntry
    {
        public Guid ToDoListId { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }
        public User User { get; set; }
    }
}
