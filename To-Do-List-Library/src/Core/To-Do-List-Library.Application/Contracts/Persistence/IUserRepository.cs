using System;
using To_Do_List_Library.Core.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace To_Do_List_Library.Core.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User> 
    {
        User LoginUser(User entity);
    }
}
