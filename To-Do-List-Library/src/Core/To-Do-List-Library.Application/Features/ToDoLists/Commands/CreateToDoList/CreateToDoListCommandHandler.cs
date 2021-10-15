using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using To_Do_List_Library.Core.Entities;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Application.Contracts.Identity;
using To_Do_List_Library.Application.Contracts.Presentation;

namespace To_Do_List_Library.Application.Features.ToDoLists.Commands.CreateToDoList
{
    public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoListCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;
        public CreateToDoListCommandHandler(IMapper mapper,
            IToDoListRepository toDoListRepository,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _toDoListRepository = toDoListRepository;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<Guid> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
        {
            var userId = _authenticationService.GetUserId(_loggedInUserService.UserId);
            var toDoList = _mapper.Map<ToDoList>(request);
            toDoList.UserId = userId;
            var addedToDoList = await _toDoListRepository.AddAsync(toDoList);
            return addedToDoList.ToDoListId;
        }
    }
}
