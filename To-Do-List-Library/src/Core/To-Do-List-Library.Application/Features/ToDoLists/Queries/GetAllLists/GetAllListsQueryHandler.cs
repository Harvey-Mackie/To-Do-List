using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Application.Contracts.Identity;
using To_Do_List_Library.Application.Contracts.Presentation;

namespace To_Do_List_Library.Application.Features.ToDoLists.Queries.GetAllLists
{
    public class GetAllListsQueryHandler : IRequestHandler<GetAllListsQuery, List<GetAllListsQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;
        public GetAllListsQueryHandler(IMapper mapper,
            IToDoListRepository toDoItemRepository,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<List<GetAllListsQueryResponse>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
        {
            var userId = _authenticationService.GetUserId(_loggedInUserService.UserId);
            var toDoList = await _toDoListRepository.GetAllWithItemsAsync(userId);
            
            if (toDoList is null) return null;

            var toDoListDto = _mapper.Map<List<GetAllListsQueryResponse>>(toDoList);
            return toDoListDto;
        }
    }
}
