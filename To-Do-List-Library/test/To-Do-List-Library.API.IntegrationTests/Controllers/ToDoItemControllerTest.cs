using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists;
using To_Do_List_Library.Presentation.API.IntegrationTests.Base;
using Xunit;

namespace To_Do_List_Library.Presentation.API.IntegrationTests.Controllers
{
    public class ToDoItemControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        public ToDoItemControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.GetAnonymousClient();
        }

        [Fact]
        public async Task Create_Should_Pass()
        {
            await Utilities.LoginUser(_client);
            var toDoListId = await Utilities.GetActiveToDoListId(_client);

            var parameters = new Dictionary<string, string>
            {
                {"ToDoListId", toDoListId.ToString() },
                {"Title", "new item"}
            };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await _client.PostAsync($"/ToDoItem/create", encodedContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var seralisedGuid = Utilities.SeraliseGuidString(responseContent);

            try
            {
                Guid.Parse(seralisedGuid);
            }
            catch
            {
                Assert.False(true, "Expected GUID");
                return;
            }
            Assert.True(true, "Recieved GUID");
        }

        [Fact]
        public async Task Delete_Should_Pass()
        {
            await Utilities.LoginUser(_client);
            var toDoItemId = await GetActiveToDoItemId();

            var parameters = new Dictionary<string, string>
            {
                { "toDoItemId", toDoItemId.ToString()}
            };
            var response = await _client.PostAsync($"/ToDoItem/delete", new FormUrlEncodedContent(parameters));
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = bool.Parse(responseContent);

            if (result is false)
            {
                Assert.False(true, "Expected a To Do List object to be deleted");
                return;
            }

            Assert.True(true, "Deleted object - no exceptions thrown");
        }

        [Fact]
        public async Task UpdateToComplete_Should_Pass()
        {
            await Utilities.LoginUser(_client);
            var toDoItemId = await GetActiveToDoItemId();

            var response = await _client.PostAsync($"/ToDoItem/{toDoItemId}/Completed", null);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = bool.Parse(responseContent);

            if (result is false)
            {
                Assert.False(true, "Expected a To Do List object to be updated");
                return;
            }

            Assert.True(true, "Updated object - no exceptions thrown");
        }

        [Fact]
        public async Task UpdateToIncomplete_Should_Pass()
        {
            await Utilities.LoginUser(_client);
            var toDoItemId = await GetActiveToDoItemId();

            var response = await _client.PostAsync($"/ToDoItem/{toDoItemId}/Incompleted", null);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = bool.Parse(responseContent);

            if (result is false)
            {
                Assert.False(true, "Expected a To Do List object to be updated");
                return;
            }

            Assert.True(true, "Updated object - no exceptions thrown");
        }


        private async Task<Guid> GetActiveToDoItemId()
        {
            await Utilities.LoginUser(_client);

            var toDoListId = await Utilities.GetActiveToDoListId(_client);
            var response = await _client.GetAsync($"/ToDoList/get?ToDoListId={toDoListId}");
            var responseContent = await response.Content.ReadAsStringAsync();

            var toDoList = JsonConvert.DeserializeObject<GetAllListsQueryResponse>(responseContent);

            return toDoList.ToDoItems.FirstOrDefault().ToDoItemId;
        }
    }
}
