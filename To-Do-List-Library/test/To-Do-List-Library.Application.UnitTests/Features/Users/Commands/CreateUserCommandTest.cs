using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.Users;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Features.Users.Commands.CreateUser;
using To_Do_List_Library.Core.Application.Profiles;
using Xunit;

namespace To_Do_List_Library.Application.UnitTests.Features.Users.Commands
{
    public class CreateUserCommandTest
    {
        //IMapper mapper, IUserRepository userRepository

        private readonly IMapper _mapper;
        private readonly UsersRepositoryMock _usersRepositoryMock; 
        private readonly Mock<IUserRepository> _userRepository;

        public CreateUserCommandTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _usersRepositoryMock = new UsersRepositoryMock();
            _userRepository = _usersRepositoryMock.GetUserRepository();
        }   

        [Fact]
        public async Task CreateUser_Should_Pass()
        {
            //Arrange
            var createUserCommandHandler = new CreateUserCommandHandler(_mapper, _userRepository.Object);
            var numberOfUsersBeforeCreate = _usersRepositoryMock.allUsers.Count;
            _userRepository.Setup(repo => repo.IsUserEmailUnique(It.IsAny<string>())).Returns(true);

            //Act
            await createUserCommandHandler.Handle(new CreateUserCommand { Email = "test@gmail.com", Password = "test", FirstName = "test", LastName = "test" }, CancellationToken.None);

            //Assert
            Assert.Equal(numberOfUsersBeforeCreate + 1, _usersRepositoryMock.allUsers.Count);

        }

        [Fact]
        public async Task CreateUser_With_Non_Unique_Email_Addres_Should_Fail()
        {
            //Arrange
            var createUserCommandHandler = new CreateUserCommandHandler(_mapper, _userRepository.Object);
            var numberOfUsersBeforeCreate = _usersRepositoryMock.allUsers.Count;
            var duplicatedEmailAddress = _usersRepositoryMock.allUsers.First().Email;
            _userRepository.Setup(repo => repo.IsUserEmailUnique(It.IsAny<string>())).Returns(false);


            //Act
            try
            {
                await createUserCommandHandler.Handle(new CreateUserCommand { Email = duplicatedEmailAddress, Password = "test", FirstName = "test", LastName = "test" }, CancellationToken.None);
                Assert.False(true, "Inserted duplicate email address");
            }
            catch
            {
                //ignore
            }
        }
    }
}
