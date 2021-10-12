using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Application.Features.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ToDoListId { get; set; }
        public string Title { get; set; }
    }
}
