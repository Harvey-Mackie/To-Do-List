using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Application.Features.ToDoItems.Commands.UpdateToDoItemToIncomplete
{
    public class UpdateToDoItemToIncompleteCommand : IRequest<bool>
    {
        public Guid ToDoItemId { get; set; }
    }
}
