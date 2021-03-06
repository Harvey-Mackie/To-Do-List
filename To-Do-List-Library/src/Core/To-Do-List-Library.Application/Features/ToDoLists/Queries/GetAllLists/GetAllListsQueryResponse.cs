using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists
{
    public class GetAllListsQueryResponse
    {
        public Guid ToDoListId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ToDoItem> ToDoItems { get; set; }
    }
}
