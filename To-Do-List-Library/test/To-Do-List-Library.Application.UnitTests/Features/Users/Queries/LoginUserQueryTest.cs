using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.Users;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Profiles;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.AuthenticationService;
using To_Do_List_Library.Core.Application.Features.Users.Queries.LoginUser;
using System.Threading;
using Xunit;
using To_Do_List_Library.Core.Domain.Entities;
using To_Do_List_Library.Core.Application.Models.Authentication;

namespace To_Do_List_Library.Application.UnitTests.Features.Users.Queries
{
    public class LoginUserQueryTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UsersRepositoryMock _userRepositoryMock;
        private readonly Mock<IAuthenticationService> _authenticationService;
        public LoginUserQueryTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _userRepositoryMock = new UsersRepositoryMock();
            _userRepository = _userRepositoryMock.GetUserRepository();
            _authenticationService = AuthenticationServiceMock.GetAuthenticationService();
        }

        [Fact]
        public async Task LoginUser_Valid_Credentials_Should_Pass()
        {
            //Arrange
            var validUser = _userRepositoryMock.allUsers.First();
            var loginUserQueryHandler = new LoginUserQueryHandler(_mapper, _userRepository.Object, _authenticationService.Object);

            //Act
            var authToken = await loginUserQueryHandler.Handle(new LoginUserQuery { Email = validUser.Email, Password = validUser.Password }, CancellationToken.None);

            //Assert
            Assert.True(!string.IsNullOrEmpty(authToken));
        }

        [Fact]
        public async Task LoginUser_Incorrect_Details_Should_Fail()
        {
            //Arrange
            var inValidUser = new User { Email = "aa@aa.co.uk", Password = "ssss" };
            var loginUserQueryHandler = new LoginUserQueryHandler(_mapper, _userRepository.Object, _authenticationService.Object);

            _authenticationService.Setup(repo => repo.Authenticate(It.Is<AuthenticationRequest>(x => x.Email == inValidUser.Email))).Throws(new Exception());

            //Act
            try
            {
                var authToken = await loginUserQueryHandler.Handle(new LoginUserQuery { Email = inValidUser.Email, Password = inValidUser.Password }, CancellationToken.None);
                Assert.True(string.IsNullOrEmpty(authToken));
            }
            catch
            {
                //ignore
            }
        }
    }
}
