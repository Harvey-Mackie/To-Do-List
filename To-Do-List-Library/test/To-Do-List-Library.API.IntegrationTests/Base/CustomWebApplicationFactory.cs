using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using To_Do_List_Library.Infrastructure.Persistence.Configuration;

namespace To_Do_List_Library.Presentation.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<ToDoDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ToDoListLibraryInMemoryTest");
                });

                var serviceProvider = services.BuildServiceProvider();
                using(var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ToDoDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitaliseDbForTests(db);
                    }
                    catch(Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
