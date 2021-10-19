using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Contracts.Presentation;

namespace To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.UpdateToDoItemToComplete
{
    public class UpdateToDoItemToCompleteCommandHandler : IRequestHandler<UpdateToDoItemToCompleteCommand, bool>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;
        public UpdateToDoItemToCompleteCommandHandler(IToDoItemRepository toDoItemRepository,
            IToDoListRepository toDoListRepository,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _toDoItemRepository = toDoItemRepository;
            _toDoListRepository = toDoListRepository;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<bool> Handle(UpdateToDoItemToCompleteCommand request, CancellationToken cancellationToken)
        {
            var isUserValid = await IsUserValid(request);

            if (isUserValid is false)
            {
                throw new Exception($"List is not associated with user");
            }

            var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId);
            
            if (toDoItem.Completed is true) 
            { 
                return true;
            }

            try
            {
                toDoItem.Completed = true;
                await _toDoItemRepository.UpdateAsync(toDoItem);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> IsUserValid(UpdateToDoItemToCompleteCommand request)
        {
            var userId = _authenticationService.GetUserId(_loggedInUserService.UserId);
            var toDoList = _toDoListRepository.GetListByItemId(request.ToDoItemId, userId);
            if (toDoList.UserId.Equals(userId))
            {
                return true;
            }
            return false;
        }
    }
}
