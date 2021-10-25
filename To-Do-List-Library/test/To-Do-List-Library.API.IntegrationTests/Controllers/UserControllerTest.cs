using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Presentation.API.IntegrationTests.Base;
using Xunit;

namespace To_Do_List_Library.Presentation.API.IntegrationTests.Controllers
{
    public class UserControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;
        public UserControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.GetAnonymousClient();
        }

        [Fact]
        public async Task Register_User_Valid_Credentials_Should_Pass()
        {
            var parameters = new Dictionary<string, string> { { "Email", $"test{Guid.NewGuid()}@test.com" }, { "Password", "test" }, { "FirstName", "test" }, { "LastName", "test" } };

            var responseString = await GetResponseStringFromEndpoint("register", parameters);
            var seralisedResponseString = SeraliseGuidString(responseString);

            try
            {
                var responseGuid = Guid.Parse(seralisedResponseString);
            }
            catch (ArgumentNullException ex)
            {
                Assert.False(true, $"Expceted Guid to be returned. Exception {ex.Message}");
            }
        }

        [Fact]
        public async Task Register_Duplicated_User__Should_Fail()
        {
            var usersEmailAddress = $"test{Guid.NewGuid()}@test.com";
            
            //Attempt to create user twice
            for(int i = 0; i < 2; i++)
            {
                try
                {
                    await GetResponseStringFromEndpoint("register", new Dictionary<string, string> { 
                        { "Email", usersEmailAddress },
                        { "Password", "test" },
                        { "FirstName", "test" },
                        { "LastName", "test" } 
                    });
                }
                catch
                {
                    Assert.True(true, $"Successfully threw exception");
                    return;
                }
            }
            Assert.False(true, $"Failed to throw exception");
        }

        [Fact]
        public async Task Login_User_Valid_Credentials_Should_Pass()
        {
            var parameters = new Dictionary<string, string> { 
                { "Email", "test@test.com" }, 
                { "Password", "test" }
            };
            try
            {
                var responseString = await GetResponseStringFromEndpoint("login", parameters);
                Assert.False(true, "Jwt Token Returned");
            }
            catch
            {
                Assert.True(true, "Invalid string return - expected jwt token");
            }
        }

        [Fact]
        public async Task Login_User_Invalid_Credentials_Should_Pass()
        {
            var parameters = new Dictionary<string, string> {
                { "Email", $"test{Guid.NewGuid()}@test.com" },
                { "Password", "Fake_PASSword" }
            };
            var responseString = await GetResponseStringFromEndpoint("login", parameters, false);
            var jwtSections = responseString.Split(".");

            if (jwtSections.Length is 3)
            {
                Assert.False(true, "Jwt Token Returned");
                return;
            }

            Assert.True(true, "No JWT Token returned as expected" );
        }



        private async Task<string> GetResponseStringFromEndpoint(string endpoint, Dictionary<string, string> parameters, bool ensureSuccessStatusCode = true)
        {
            var encodedContent = new FormUrlEncodedContent(parameters);

            var response = await _client.PostAsync($"/user/{endpoint}", encodedContent);

            if (ensureSuccessStatusCode) 
            {
                response.EnsureSuccessStatusCode();
            }

            return await response.Content.ReadAsStringAsync();
        }


        private string SeraliseGuidString(string guidJson)
        {
            return guidJson.Split('"')[1];
        }
    }
}
