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

namespace To_Do_List_Library.Application.Features.ToDoLists.Commands.CreateToDoList
{
    public class CreateToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IToDoListRepository _toDoListRepository;
        public CreateToDoListCommandHandler(IMapper mapper, IToDoListRepository toDoListRepository)
        {
            _mapper = mapper;
            _toDoListRepository = toDoListRepository;
        }
        public async Task<bool> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
        {
            var toDoList = _mapper.Map<ToDoList>(request);
            await _toDoListRepository.AddAsync(toDoList);
            return true;
        }
    }
}
