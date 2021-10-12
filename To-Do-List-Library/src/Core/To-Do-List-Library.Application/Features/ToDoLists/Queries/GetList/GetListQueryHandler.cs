using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Application.Contracts.Persistence;

namespace To_Do_List_Library.Application.Features.ToDoLists.Queries.GetList
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, GetListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        public GetListQueryHandler(IMapper mapper, IToDoListRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
        }

        public async Task<GetListQueryResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var toDoList = await _toDoListRepository.GetByIdAsync(request.ListId);
            var toDoListResponse = _mapper.Map<GetListQueryResponse>(toDoList);
            return toDoListResponse;
        }
    }
}
