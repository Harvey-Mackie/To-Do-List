using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Persistence.IntegrationTests
{
    public class ToDoDbContextTest
    {
        private readonly ToDoDbContext _toDoDbContext;
        private readonly Mock<ILoggedInUserService>  _loggedInUserService;
        private readonly string _loggedInUserId;

        public ToDoDbContextTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ToDoDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserService = new Mock<ILoggedInUserService>();

            _loggedInUserId = "00000000-0000-0000-0000-000000000000"; 
            _loggedInUserService.Setup(x => x.UserId).Returns(_loggedInUserId);

            _toDoDbContext = new ToDoDbContext(dbContextOptions, _loggedInUserService.Object);
        }

        [Fact]
        public async Task Save_SetCreatedBy_Property()
        {
            var newUser = new User { Email = "test123@test.com", Password = "test" };
            _toDoDbContext.User.Add(newUser);
            await _toDoDbContext.SaveChangesAsync();

            Assert.True(newUser.CreatedBy == _loggedInUserId);
        }
    }
}
