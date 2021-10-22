using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using To_Do_List_Library.Application.UnitTests.Mocks.Data;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Repositories.Users
{
    public class UsersRepositoryMock
    {
        public List<User> allUsers;

        public UsersRepositoryMock()
        {
            allUsers = new List<User> { UsersMock.GetUser() };
        }

        public Mock<IUserRepository> GetUserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(
                (User user) =>
                {
                    allUsers.Add(user);
                    return user;
                });

            userRepositoryMock.Setup(repo => repo.LoginUser(It.IsAny<User>())).Returns((User user) => user);


            return userRepositoryMock;
        }
    }
}
