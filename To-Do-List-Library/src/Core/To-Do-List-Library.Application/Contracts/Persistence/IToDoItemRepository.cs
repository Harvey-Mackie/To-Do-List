using System;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Core.Application.Contracts.Persistence
{
    public interface IToDoItemRepository : IAsyncRepository<ToDoItem>
    {
    }
}
