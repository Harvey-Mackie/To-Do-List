using AutoMapper;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoItems;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoLists;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.AuthenticationService;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.LoggedInUserService;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.UpdateToDoItemToIncomplete;
using To_Do_List_Library.Core.Application.Profiles;
using To_Do_List_Library.Core.Domain.Entities;
using Xunit;

namespace To_Do_List_Library.Application.UnitTests.Features.ToDoItems.Commands
{
    public class UpdateToDoItemToIncompleteCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IToDoItemRepository> _toDoItemRepository;
        private readonly Mock<IToDoListRepository> _toDoListRepository;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly Mock<ILoggedInUserService> _loggedInService;

        private ToDoListRepositoryMock _toDoListRepositoryMock;
        private ToDoItemRepositoryMock _toDoItemRepositoryMock;

        public UpdateToDoItemToIncompleteCommandTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            
            _toDoListRepositoryMock = new ToDoListRepositoryMock();
            _toDoListRepository = _toDoListRepositoryMock.GetToDoListRepository();
            _toDoItemRepositoryMock = new ToDoItemRepositoryMock();
            _toDoItemRepository = _toDoItemRepositoryMock.GetToDoItemRepository();

            _authenticationService = AuthenticationServiceMock.GetAuthenticationService();
            _loggedInService = LoggedInUserServiceMock.GetLoggedInUserService();
        }

        [Fact]
        public async Task UpdateToDoItemToComplete_Should_Pass()
        {
            //Arrange
            var toDoItem = _toDoItemRepositoryMock.toDoItemValues.First();
            var toDoList = _toDoItemRepositoryMock.toDoListValue;
            var toDoItemOldCompletedValue = true;
            toDoItem.Completed = toDoItemOldCompletedValue;

            var updateToDoItemCommandHandler = new UpdateToDoItemToIncompleteCommandHandler(_toDoItemRepository.Object, _toDoListRepository.Object, _authenticationService.Object, _loggedInService.Object);
            
            _toDoListRepository.Setup(repo => repo.GetListByItemId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(toDoList);
            _toDoItemRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ToDoItem>())).Returns(() => { 
                _toDoItemRepositoryMock.toDoItemValues.First().Completed = false;
                return Task.CompletedTask;
            });

            //Act
            await updateToDoItemCommandHandler.Handle(new UpdateToDoItemToIncompleteCommand { ToDoItemId = toDoItem.ToDoItemId }, CancellationToken.None);

            //Assert
            Assert.NotEqual(toDoItemOldCompletedValue, _toDoItemRepositoryMock.toDoItemValues.First().Completed);
        }
    }
}
