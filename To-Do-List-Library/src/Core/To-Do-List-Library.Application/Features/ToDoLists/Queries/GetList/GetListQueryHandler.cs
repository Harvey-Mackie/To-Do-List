using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Contracts.Presentation;

namespace To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, GetListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;
        public GetListQueryHandler(IMapper mapper, 
            IToDoListRepository toDoItemRepository,
            IAuthenticationService authenticationService,
            ILoggedInUserService loggedInUserService)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
            _authenticationService = authenticationService;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<GetListQueryResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var userId = _authenticationService.GetUserId(_loggedInUserService.UserId);
            var toDoList = _toDoListRepository.GetWithItemsAsync(request.ToDoListId, userId);
            var toDoListResponse = _mapper.Map<GetListQueryResponse>(toDoList);
            return toDoListResponse;
        }
    }
}
