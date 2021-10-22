using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using To_Do_List_Library.Application.UnitTests.Mocks.Data;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoItems
{
    public class ToDoItemRepositoryMock
    {
        public ICollection<ToDoItem> toDoItemValues;
        public ToDoList toDoListValue;

        public ToDoItemRepositoryMock()
        {
            toDoListValue = ToDoListsMock.GetToDoListData().First();
            toDoItemValues = toDoListValue.ToDoItems;
        }

        public Mock<IToDoItemRepository> GetToDoItemRepository()
        {
            var toDoItemRepositoryMock = new Mock<IToDoItemRepository>();

            toDoItemRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ToDoItem>())).ReturnsAsync(
                (ToDoItem toDoItem) =>
                {
                    toDoItemValues.Add(toDoItem);
                    return toDoItem;
                });

            toDoItemRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<ToDoItem>())).Returns(
                (ToDoItem toDoItem) =>
                {
                    toDoItemValues.Remove(toDoItemValues.First());
                    return Task.CompletedTask;
                });

            toDoItemRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(toDoItemValues.First());

            return toDoItemRepositoryMock;
        }
    }
}
