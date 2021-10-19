using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists;
using MediatR;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.CreateToDoList;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.DeleteToDoList;
using Microsoft.AspNetCore.Authorization;
using To_Do_List_Library.Presentation.API.Controllers.Filters;
using Microsoft.Net.Http.Headers;

namespace To_Do_List_Library.Presentation.API.Controllers
{
    [ServiceFilter(typeof(AuthTokenFilter))]
    [Route("ToDoList")]
    public class ToDoListController : Controller
    {
        private readonly IMediator _mediator;
        public ToDoListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<List<GetAllListsQueryResponse>> GetAll()
        {
            var toDoLists = await _mediator.Send(new GetAllListsQuery());
            return toDoLists;
        }

        [HttpGet("Get")]
        public async Task<GetListQueryResponse> Get(Guid ToDoListId)
        {
            var toDoList = await _mediator.Send(new GetListQuery { ToDoListId = ToDoListId});
            return toDoList;
        }

        [HttpPost("Create")]
        public async Task<Guid> Create(string name)
        {
            var isListCreated = await _mediator.Send(new CreateToDoListCommand {Name = name});
            return isListCreated;
        }

        [HttpPost("Delete")]
        public async Task<bool> Delete(Guid toDoListId )
        {
            var isListDeleted = await _mediator.Send(new DeleteToDoListCommand { ToDoListId = toDoListId });
            return isListDeleted;
        }
    }
}
