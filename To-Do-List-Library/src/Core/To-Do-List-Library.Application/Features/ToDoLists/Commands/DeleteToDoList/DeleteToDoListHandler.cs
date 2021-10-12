using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Application.Features.ToDoLists.Commands.DeleteToDoList
{
    public class DeleteToDoListHandler : IRequestHandler<DeleteToDoListCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        public DeleteToDoListHandler(IMapper mapper, IToDoListRepository toDoItemRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoItemRepository;
        }
        public async Task<bool> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoListEntity = _mapper.Map<ToDoList>(request);
            await _toDoListRepository.DeleteAsync(toDoListEntity);
            return true;
        }
    }
}
