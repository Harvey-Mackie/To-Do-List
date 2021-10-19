using MediatR;
using System;

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.CreateToDoList
{
    public class CreateToDoListCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
