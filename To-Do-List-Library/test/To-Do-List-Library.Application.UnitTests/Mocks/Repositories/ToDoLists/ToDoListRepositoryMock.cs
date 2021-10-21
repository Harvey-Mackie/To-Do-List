using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoLists
{
    public class ToDoListRepositoryMock
    {
        public List<ToDoList> toDoListValues;

        public ToDoListRepositoryMock()
        {
            toDoListValues = ToDoListRepositoryMockData.GetToDoListData();
        }

        public Mock<IToDoListRepository> GetToDoListRepository()
        {
            var mockToDoListRepository = new Mock<IToDoListRepository>();

            mockToDoListRepository.Setup(repo => repo.GetAllWithItemsAsync(It.IsAny<Guid>() )).ReturnsAsync(toDoListValues);
            mockToDoListRepository.Setup(repo => repo.GetListByItemId(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(toDoListValues[0]);
            mockToDoListRepository.Setup(repo => repo.GetWithItemsAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(toDoListValues[0]);

            mockToDoListRepository.Setup(repo => repo.AddAsync(It.IsAny<ToDoList>())).ReturnsAsync(
                (ToDoList toDoList) =>
                {
                    toDoListValues.Add(toDoList);
                    return toDoList;
                });

            mockToDoListRepository.Setup(repo => repo.DeleteAsync( It.IsAny<ToDoList>() )).Returns( 
                (ToDoList toDoList) =>
                {
                    toDoListValues.Remove(toDoListValues[0]);
                    return Task.CompletedTask;
                }    
            );

            return mockToDoListRepository;
        }
    }
}
