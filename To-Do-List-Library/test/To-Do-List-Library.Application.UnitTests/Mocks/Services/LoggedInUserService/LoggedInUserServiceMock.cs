using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using Moq;
namespace To_Do_List_Library.Application.UnitTests.Mocks.Services.LoggedInUserService
{
    public static class LoggedInUserServiceMock
    {
        public static Mock<ILoggedInUserService> GetLoggedInUserService()
        {
            var loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            loggedInUserServiceMock.SetupAllProperties();
            loggedInUserServiceMock.Object.UserId = Guid.Parse("590ff622-d710-4cd4-97dc-d7c73519da5d").ToString();
            return loggedInUserServiceMock;
        }
    }
}
