using AutoMapper;
using System.Collections.Generic;
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.CreateToDoItem;
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.DeleteToDoItem;
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.UpdateToDoItemToComplete;
using To_Do_List_Library.Core.Application.Features.ToDoItems.Commands.UpdateToDoItemToIncomplete;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.CreateToDoList;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Commands.DeleteToDoList;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetAllLists;
using To_Do_List_Library.Core.Application.Features.ToDoLists.Queries.GetList;
using To_Do_List_Library.Core.Application.Features.Users.Commands.CreateUser;
using To_Do_List_Library.Core.Application.Features.Users.Queries.LoginUser;
using To_Do_List_Library.Core.Application.Models.Authentication;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Core.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<AuthenticationRequest, User>().ReverseMap();
            CreateMap<AuthenticationRequest, LoginUserQuery>().ReverseMap();

            CreateMap<CreateToDoItemCommand, ToDoItem>().ReverseMap();
            CreateMap<DeleteToDoItemCommand, ToDoItem>().ReverseMap();

            CreateMap<CreateToDoListCommand, ToDoList>().ReverseMap();
            CreateMap<DeleteToDoListCommand, ToDoList>().ReverseMap();
            CreateMap<GetListQueryResponse, ToDoList>().ReverseMap();
            CreateMap<GetAllListsQueryResponse, ToDoList>().ReverseMap();
        }
    }
}
