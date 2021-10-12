using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;
using To_Do_List_Library.Persistence.Configuration;

namespace To_Do_List_Library.Persistence.Repositories
{
    public class ToDoListRepository : BaseRepository<ToDoList>, IToDoListRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public ToDoListRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }

        public Task<List<ToDoList>> GetAllAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
