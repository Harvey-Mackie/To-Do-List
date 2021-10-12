using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Persistence.Configuration
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);

            var toDoListGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var toDoItemGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var userGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            
            var toDoItems = new List<ToDoItem>
                {
                    new ToDoItem
                    {
                        ToDoItemId = toDoItemGuid,
                        Completed = false,
                        Title = "First Item on the list"
                    }
                };
            var user = new User
            {
                UserId = userGuid,
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                Password = "test"
            };
            var toDoList = new ToDoList
            {
                ToDoListId = toDoListGuid,
                Name = "Test To Do List",
                ToDoItems = toDoItems,
                UserId = userGuid
            };

            modelBuilder.Entity<ToDoItem>().HasData(toDoItems);
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<ToDoList>().HasData(toDoList);
        }
    }
}
