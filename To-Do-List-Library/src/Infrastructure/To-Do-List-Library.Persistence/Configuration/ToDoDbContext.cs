using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using To_Do_List_Library.Core.Application.Contracts.Presentation;
using To_Do_List_Library.Core.Domain.Common;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Infrastructure.Persistence.Configuration
{
    public class ToDoDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
           : base(options)
        {
        }
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options, ILoggedInUserService loggedInUserService)
          : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);

            var toDoListGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var toDoItemGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var userGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");

            modelBuilder.Entity<ToDoItem>().HasData(new ToDoItem
            {
                ToDoItemId = toDoItemGuid,
                Completed = false,
                Title = "First Item on the list",
                ToDoListId = toDoListGuid
            });
            modelBuilder.Entity<ToDoItem>().HasData(new ToDoItem
            {
                ToDoItemId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDD}"),
                Completed = false,
                Title = "Second Item on the list",
                ToDoListId = toDoListGuid
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = userGuid,
                Email = "test@test.com",
                FirstName = "test",
                LastName = "test",
                Password = "test"
            });
            
            modelBuilder.Entity<ToDoList>().HasData(new ToDoList
            {
                ToDoListId = toDoListGuid,
                Name = "Test To Do List",
                UserId = userGuid
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entity in ChangeTracker.Entries<AuditableEntry>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.Now;
                        entity.Entity.CreatedBy = _loggedInUserService.UserId;
                        break; 
                    case EntityState.Modified:
                        entity.Entity.LastModifiedDate = DateTime.Now;
                        entity.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
