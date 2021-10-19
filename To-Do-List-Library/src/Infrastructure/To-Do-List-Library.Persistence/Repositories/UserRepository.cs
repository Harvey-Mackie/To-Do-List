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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ToDoDbContext _toDoDbContext;
        public UserRepository(ToDoDbContext toDoDbContext) : base(toDoDbContext)
        {
            _toDoDbContext = toDoDbContext;
        }
        public User LoginUser(User entity)
        {
            var user = _toDoDbContext.User.Where(x => x.Email == entity.Email && x.Password == entity.Password).FirstOrDefault();
            if(user == null)
            {
                throw new Exception("Incorrect Credentials");
            }
            return user;
        }
    }
}
