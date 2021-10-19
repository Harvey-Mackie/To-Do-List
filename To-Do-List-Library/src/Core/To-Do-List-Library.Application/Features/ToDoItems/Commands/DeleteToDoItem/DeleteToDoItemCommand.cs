using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.DeleteToDoItem
{
    public class DeleteToDoItemCommand : IRequest<bool>
    {
        public Guid ToDoItemId { get; set; }
    }
}
