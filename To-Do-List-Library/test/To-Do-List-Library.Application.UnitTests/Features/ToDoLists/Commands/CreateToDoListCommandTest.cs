using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoLists;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.AuthenticationService;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.LoggedInUserService;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.CreateToDoList;
using To_Do_List_Library.Core.Application.Profiles;
using Xunit;

namespace To_Do_List_Library.Application.UnitTests.Features.ToDoLists.Commands
{
    public class CreateToDoListCommandTest
    {
        private readonly IMapper _mapper;
        private readonly ToDoListRepositoryMock _toDoListRepositoryMock;
        private readonly Mock<IToDoListRepository> _toDoListRepository;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly Mock<ILoggedInUserService> _loggedInUserService;

        public CreateToDoListCommandTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _toDoListRepositoryMock = new ToDoListRepositoryMock();
            _toDoListRepository = _toDoListRepositoryMock.GetToDoListRepository();
            _authenticationService = AuthenticationServiceMock.GetAuthenticationService();
            _loggedInUserService = LoggedInUserServiceMock.GetLoggedInUserService();
        }

        [Fact]
        public async Task CreateToDoList_Should_Pass()
        {
            //Arrange
            int toDoListCollectionCount = _toDoListRepositoryMock.toDoListValues.Count;
            var createToDoListHandler = new CreateToDoListCommandHandler(_mapper, _toDoListRepository.Object, _authenticationService.Object, _loggedInUserService.Object);

            //Act
            var createToDoListResponse = await createToDoListHandler.Handle(new CreateToDoListCommand { Name="Forth Item" }, CancellationToken.None);

            //Assert
            Assert.IsType<Guid>(createToDoListResponse);
            Assert.Equal(toDoListCollectionCount + 1, _toDoListRepositoryMock.toDoListValues.Count);

        }

    }
}
