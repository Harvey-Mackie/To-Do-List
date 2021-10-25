using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists;
using To_Do_List_Library.Core.Domain.Entities;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;

namespace To_Do_List_Library.Presentation.API.IntegrationTests.Base
{
    internal class Utilities
    {
        public static void InitaliseDbForTests(ToDoDbContext toDoDbContext)
        {
            var toDoListGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var toDoItemGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var userGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");

            toDoDbContext.User.Add(new User
            {
                UserId = userGuid,
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                Password = "test"
            });

            toDoDbContext.ToDoList.Add(new ToDoList
            {
                ToDoListId = toDoListGuid,
                Name = "Test To Do List",
                UserId = userGuid,
                ToDoItems = new List<ToDoItem>
                {
                    new ToDoItem
                    {
                        ToDoItemId = toDoItemGuid,
                        Completed = false,
                        Title = "First Item on the list",
                        ToDoListId = toDoListGuid
                    }
                }
            });          
        }
        public static string SeraliseGuidString(string guidJson)
        {
            return guidJson.Split('"')[1];
        }
    
        public static async Task<Guid> GetActiveToDoListId(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"/ToDoList/getall");
            var responseContent = await response.Content.ReadAsStringAsync();

            var toDoLists = JsonConvert.DeserializeObject<List<GetAllListsQueryResponse>>(responseContent);

            return toDoLists.FirstOrDefault().ToDoListId;
        }

        public static async Task LoginUser(HttpClient httpClient)
        {
            var parameters = new Dictionary<string, string>
            {
                {"email", "test@test.com"},
                {"password", "test"}
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await httpClient.PostAsync($"/user/login", encodedContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(responseContent);
        }
    }
}
