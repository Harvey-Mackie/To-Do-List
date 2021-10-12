using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Application.Contracts.Persistence
{
    public interface IToDoListRepository : IAsyncRepository<ToDoList>
    {
        Task<List<ToDoList>> GetAllAsync(Guid userId);
    }
}
