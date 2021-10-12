using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Application.Features.ToDoLists.Commands.DeleteToDoList
{
    public class DeleteToDoListCommand : IRequest<bool>
    {
        public Guid ToDoListId { get; set; }
    }
}
