using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList
{
    public class GetListQueryResponse 
    {
        public Guid ToDoListId { get; set; }
        public string Name { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }
    }
}
