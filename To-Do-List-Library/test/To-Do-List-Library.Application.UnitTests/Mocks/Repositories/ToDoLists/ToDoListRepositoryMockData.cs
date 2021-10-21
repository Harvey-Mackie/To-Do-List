using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Application.UnitTests.Mocks.Repositories.ToDoLists
{
    public static class ToDoListRepositoryMockData
    { 
        internal static List<ToDoList> GetToDoListData()
        {
            var userGuid = Guid.Parse("590ff622-d710-4cd4-97dc-d7c73519da5d");

            return new List<ToDoList>
            {
                new ToDoList
                {
                    Name = "First List",
                    UserId = userGuid,
                    ToDoListId = Guid.Parse("690ff622-d710-4cd4-97dc-d7c73519da5e"),
                    ToDoItems = new List<ToDoItem>
                    {
                        new ToDoItem
                        {
                            ToDoListId = Guid.Parse("690ff622-d710-4cd4-88dc-d7c73519da5e"),
                            Completed = false,
                            Title = "First Item"
                        }
                    }
                },
                new ToDoList
                {
                    Name = "Second List",
                    UserId = userGuid,
                },
                new ToDoList
                {
                    Name = "Third List",
                    UserId = userGuid
                }
            };
        }
    }
}
