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

namespace To_Do_List_Library.Application.Features.ToDoItems.Commands.CreateToDoItem
{
    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IToDoItemRepository _toDoItemRepository;
        private readonly IToDoListRepository _toDoListRepository;
        public CreateToDoItemCommandHandler(IMapper mapper, IToDoItemRepository toDoItemRepository, IToDoListRepository toDoListRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
            _toDoListRepository = toDoListRepository;
        }
        public async Task<Guid> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var isUserValid = await IsUserValid(request);
            
            if(isUserValid is false)
            {
                throw new Exception($"List is not associated with user {request.UserId}");
            }

            var toDoListItem = _mapper.Map<ToDoItem>(request);
            var toDoListItemAdded = await _toDoItemRepository.AddAsync(toDoListItem);

            return toDoListItemAdded.ToDoItemId;
        }

        private async Task<bool> IsUserValid(CreateToDoItemCommand request)
        {
            var toDoList = await _toDoListRepository.GetByIdAsync(request.ToDoListId);
            if (toDoList.UserId.Equals(request.UserId) )
            {
                return true;
            }
            return false;
        }
    }
}
