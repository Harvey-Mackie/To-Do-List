using MediatR;

namespace To_Do_List_Library.Application.Features.ToDoLists.Commands.CreateToDoList
{
    public class DeleteToDoListCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
