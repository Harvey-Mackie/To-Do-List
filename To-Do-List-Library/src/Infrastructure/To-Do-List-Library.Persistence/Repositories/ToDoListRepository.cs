using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;

namespace To_Do_List_Library.Infrastructure.Persistence.Repositories
{
    public class ToDoListRepository : BaseRepository<ToDoList>, IToDoListRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public ToDoListRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public async Task<List<ToDoList>> GetAllWithItemsAsync(Guid userId)
        {
            return _toDoDbContext.ToDoList.Where(x=> x.UserId == userId).Include(x => x.ToDoItems).ToList();
        }

        public ToDoList GetListByItemId(Guid toDoItemId, Guid userId)
        {
            return _toDoDbContext.ToDoList
                .Where(x => x.UserId == userId)
                .Include(x => x.ToDoItems)
                .Where(x => x.ToDoItems.Any(x => x.ToDoItemId == toDoItemId))
                .First();
        }

        public ToDoList GetWithItemsAsync(Guid toDoListId, Guid userId)
        {
            return (ToDoList) _toDoDbContext.ToDoList.Where(x=>x.ToDoListId == toDoListId && x.UserId == userId).Include(x => x.ToDoItems).First();
        }
    }
}
