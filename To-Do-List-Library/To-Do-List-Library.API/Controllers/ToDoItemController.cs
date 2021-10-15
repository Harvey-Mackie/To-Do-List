using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using To_Do_List_Library.API.Controllers.Filters;
using To_Do_List_Library.Application.Features.ToDoItems.Commands.CreateToDoItem;
using To_Do_List_Library.Application.Features.ToDoItems.Commands.DeleteToDoItem;
using To_Do_List_Library.Application.Features.ToDoItems.Commands.UpdateToDoItemToComplete;
using To_Do_List_Library.Application.Features.ToDoItems.Commands.UpdateToDoItemToIncomplete;

namespace To_Do_List_Library.API.Controllers
{
    [ServiceFilter(typeof(AuthTokenFilter))]
    [Route("ToDoItem")]
    public class ToDoItemController : Controller
    {
        private readonly IMediator _mediator;
        public ToDoItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<Guid> Create(CreateToDoItemCommand createToDoItemCommand)
        {
            var isListCreated = await _mediator.Send(createToDoItemCommand);
            return isListCreated;
        }

        [HttpPost("Delete")]
        public async Task<bool> Delete(Guid toDoItemId)
        {
            var isListDeleted = await _mediator.Send(new DeleteToDoItemCommand { ToDoItemId = toDoItemId});
            return isListDeleted;
        }

        [HttpPost("{toDoItemId}/Completed")]
        public async Task<bool> Completed(Guid toDoItemId)
        {
            var isCompleted = await _mediator.Send(new UpdateToDoItemToCompleteCommand { ToDoItemId = toDoItemId});
            return isCompleted;
        }


        [HttpPost("{toDoItemId}/Incompleted")]
        public async Task<bool> Incompleted(Guid toDoItemId)
        {
            var isIncompleted = await _mediator.Send(new UpdateToDoItemToIncompleteCommand { ToDoItemId = toDoItemId});
            return isIncompleted;
        }
    }
}
