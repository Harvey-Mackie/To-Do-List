using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Profiles;
using To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoLists;
using Xunit;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Infrastructure.Identity.Services;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using System.Threading;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.LoggedInUserService;
using To_Do_List_Library.Application.UnitTests.Mocks.Services.AuthenticationService;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList;

namespace To_Do_List_Library.Application.UnitTests.Features.ToDoLists.Queries
{
    public class GetListQueryQueryTest
    {
        private readonly IMapper _mapper;
        private readonly ToDoListRepositoryMock _toDoListRepositoryMock;
        private readonly Mock<IToDoListRepository> _toDoListRepository;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly Mock<ILoggedInUserService> _loggedInUserService;

        public GetListQueryQueryTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _toDoListRepositoryMock = new ToDoListRepositoryMock();
            _toDoListRepository = _toDoListRepositoryMock.GetToDoListRepository();
            _authenticationService = AuthenticationServiceMock.GetAuthenticationService();
            _loggedInUserService = LoggedInUserServiceMock.GetLoggedInUserService();
        }

        [Fact]
        public async Task GetAllWithItemsAsync_Should_Pass()
        {
            //Arrange
            var toDoListCollection = _toDoListRepositoryMock.toDoListValues;
            var getListQueryHandler = new GetListQueryHandler(_mapper, _toDoListRepository.Object, _authenticationService.Object, _loggedInUserService.Object);

            //Act
            var toDoListQueryResponse = await getListQueryHandler.Handle(new GetListQuery {ToDoListId = toDoListCollection[0].ToDoListId }, CancellationToken.None);

            //Assert
            Assert.Equal(toDoListCollection[0].ToDoListId, toDoListQueryResponse.ToDoListId);
            Assert.Equal(toDoListCollection[0].Name, toDoListQueryResponse.Name);
            Assert.Equal(toDoListCollection[0].ToDoItems.Count, toDoListQueryResponse.ToDoItems.Count);
            Assert.Equal(toDoListCollection[0].ToDoItems.FirstOrDefault().ToDoItemId, toDoListQueryResponse.ToDoItems.FirstOrDefault().ToDoItemId);
        }
    }
}
