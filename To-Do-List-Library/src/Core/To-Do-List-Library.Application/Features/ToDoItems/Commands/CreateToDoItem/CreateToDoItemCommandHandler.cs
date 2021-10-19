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

namespace To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInService;
        public CreateToDoItemCommandHandler(IMapper mapper, 
            IToDoItemRepository toDoItemRepository, 
            IToDoListRepository toDoListRepository, 
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInService)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
            _toDoListRepository = toDoListRepository;
            _authenticationService = authenticationService;
            _loggedInService = loggedInService;
        }
        public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var isUserValid = await IsUserValid(request);
            
            if(isUserValid is false)
            {
                throw new Exception($"List is not associated with user");
            }

            var toDoListItem = _mapper.Map<ToDoItem>(request);
            var toDoListItemAdded = await _toDoItemRepository.AddAsync(toDoListItem);

            return toDoListItemAdded.ToDoItemId;
        }

        private async Task<bool> IsUserValid(CreateToDoItemCommand request)
        {
            var toDoList = await _toDoListRepository.GetByIdAsync(request.ToDoListId);
            var userId = _authenticationService.GetUserId(_loggedInService.UserId);
            if (toDoList.UserId.Equals(userId))
            {
                return true;
            }
            return false;
        }
    }
}
