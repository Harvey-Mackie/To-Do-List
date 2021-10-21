using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using To_Do_List_Library.Application.UnitTests.Mocks.Data;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Models.Authentication;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Services.AuthenticationService
{
    public static class AuthenticationServiceMock
    {
        public static Mock<IAuthenticationService> GetAuthenticationService()
        {
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            authenticationServiceMock.Setup(repo => repo.Authenticate(It.IsAny<AuthenticationRequest>())).Returns(JwtSecurityTokenMock.GetValidJwtSecurityToken());
            authenticationServiceMock.Setup(repo => repo.GetUserId(It.IsAny<string>())).Returns(UsersMock.GetUser().UserId);

            return authenticationServiceMock;
        }
    }
}
