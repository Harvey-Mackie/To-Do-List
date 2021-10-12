using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using To_Do_List_Library.Application.Contracts.Persistence;

namespace To_Do_List_Library.Application.Features.ToDoLists.Queries.GetAllLists
{
    public class GetAllListsQueryHandler : IRequestHandler<GetAllListsQuery, List<GetAllListsQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        public GetAllListsQueryHandler(IMapper mapper, IToDoListRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
        }
        public async Task<List<GetAllListsQueryResponse>> Handle(GetAllListsQuery request, CancellationToken cancellationToken)
        {
            var toDoList = await _toDoListRepository.GetAllAsync(request.UserId);
            var toDoListDto = _mapper.Map<List<GetAllListsQueryResponse>>(toDoList);
            return toDoListDto;
        }
    }
}
