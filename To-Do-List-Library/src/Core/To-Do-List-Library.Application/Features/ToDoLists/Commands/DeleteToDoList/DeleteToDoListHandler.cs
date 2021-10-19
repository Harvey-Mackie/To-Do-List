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

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.DeleteToDoList
{
    public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;
        public DeleteToDoListHandler(IMapper mapper, 
            IToDoListRepository toDoItemRepository,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<bool> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var userId = _authenticationService.GetUserId(_loggedInUserService.UserId);
            var toDoListEntity = _mapper.Map<ToDoList>(request);
            toDoListEntity.UserId = userId;
            await _toDoListRepository.DeleteAsync(toDoListEntity);
            return true;
        }
    }
}
