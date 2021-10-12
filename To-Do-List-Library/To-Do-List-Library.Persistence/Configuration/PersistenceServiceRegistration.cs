﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Persistence.Repositories;

namespace To_Do_List_Library.Persistence.Configuration
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToDoListConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            services.AddScoped<IToDoListRepository, ToDoListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}