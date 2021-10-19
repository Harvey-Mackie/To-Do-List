using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Domain.Entities;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;

namespace To_Do_List_Library.Infrastructure.Persistence.Repositories
{
    public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public ToDoItemRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
    }
}
