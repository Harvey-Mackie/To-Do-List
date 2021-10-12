using System;
using To_Do_List_Library.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace To_Do_List_Library.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User> 
    {
        Task<User> LoginUser(User entity);
    }
}
