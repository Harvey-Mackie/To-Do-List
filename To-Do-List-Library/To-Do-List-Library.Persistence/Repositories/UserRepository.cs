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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public UserRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public Task<User> LoginUser(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
