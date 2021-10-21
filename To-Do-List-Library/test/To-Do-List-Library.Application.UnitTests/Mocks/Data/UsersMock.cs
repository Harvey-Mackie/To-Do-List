using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Data
{
    public static class UsersMock
    {
        public static User GetUser()
        {
            return new User
            {
                UserId = Guid.Parse("590ff622-d710-4cd4-97dc-d7c73519da5d"),
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                Password = "Test"
            };
        } 
    }
}
