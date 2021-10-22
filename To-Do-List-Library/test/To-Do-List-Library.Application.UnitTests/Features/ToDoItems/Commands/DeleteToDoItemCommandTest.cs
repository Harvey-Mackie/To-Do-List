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
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.DeleteToDoItem;
using To_Do_List_Library.Core.Application.Profiles;
using Xunit;

namespace To_Do_List_Library.Application.UnitTests.Features.ToDoItems.Commands
{
    public class DeleteToDoItemCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IToDoItemRepository> _toDoItemRepository;
        private readonly Mock<IToDoListRepository> _toDoListRepository;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly Mock<ILoggedInUserService> _loggedInService;

        private ToDoListRepositoryMock _toDoListRepositoryMock;
        private ToDoItemRepositoryMock _toDoItemRepositoryMock;

        public DeleteToDoItemCommandTest()
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
        public async Task CreateToDoItem_Should_Pass()
        {
            //Arrange
            var toDoItemCollectionCount = _toDoItemRepositoryMock.toDoItemValues.Count;
            var deleteToDoItemCommandHandler = new DeleteToDoItemCommandHandler(_toDoItemRepository.Object, _toDoListRepository.Object, _mapper, _authenticationService.Object, _loggedInService.Object);
            _toDoListRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => { return _toDoItemRepositoryMock.toDoListValue;});

            //Act
            await deleteToDoItemCommandHandler.Handle(new DeleteToDoItemCommand { ToDoItemId=_toDoItemRepositoryMock.toDoItemValues.First().ToDoItemId }, CancellationToken.None);

            //Assert
            Assert.Equal(toDoItemCollectionCount - 1, _toDoItemRepositoryMock.toDoItemValues.Count);
        }
    }
}
