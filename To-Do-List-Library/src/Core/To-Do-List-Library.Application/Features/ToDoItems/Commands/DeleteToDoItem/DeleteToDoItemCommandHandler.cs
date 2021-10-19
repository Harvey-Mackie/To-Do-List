using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Domain.Entities;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Contracts.Presentation;

namespace To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.DeleteToDoItem
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, bool>
    {
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;

        public DeleteToDoItemCommandHandler(IToDoItemRepository toDoItemRepository,
            IToDoListRepository toDoListRepository,
            IMapper mapper,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _toDoItemRepository = toDoItemRepository;
            _toDoListRepository = toDoListRepository;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<bool> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var isUserValid = await IsUserValid(request);

            if (isUserValid is false)
            {
                throw new Exception($"List is not associated with user");
            }
            var toDoItem = await _toDoItemRepository.GetByIdAsync(request.ToDoItemId);

            try
            {
                await _toDoItemRepository.DeleteAsync(toDoItem);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private async Task<bool> IsUserValid(DeleteToDoItemCommand request)
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
