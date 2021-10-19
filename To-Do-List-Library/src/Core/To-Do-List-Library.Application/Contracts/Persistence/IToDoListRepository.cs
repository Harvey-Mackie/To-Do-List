using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Core.Application.Contracts.Persistence
{
    public interface IToDoListRepository : IAsyncRepository<ToDoList>
    {
        Task<List<ToDoList>> GetAllWithItemsAsync(Guid userId);
        ToDoList GetWithItemsAsync(Guid toDoListId, Guid userId);
        ToDoList GetListByItemId(Guid toDoItemId, Guid userId);
    }
}
