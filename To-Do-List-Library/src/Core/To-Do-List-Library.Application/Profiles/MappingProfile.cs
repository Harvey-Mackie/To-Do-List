using AutoMapper;
using System.Collections.Generic;
using To_Do_List_Library.Application.Features.ToDoItems.Commands.CreateToDoItem;
using To_Do_List_Library.Application.Features.ToDoLists.Commands.CreateToDoList;
using To_Do_List_Library.Application.Features.ToDoLists.Queries.GetAllLists;
using To_Do_List_Library.Application.Features.ToDoLists.Queries.GetList;
using To_Do_List_Library.Application.Features.Users.Commands.CreateUser;
using To_Do_List_Library.Application.Features.Users.Queries.LoginUser;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<LoginUserQuery, User>().ReverseMap();
            
            CreateMap<CreateToDoItemCommand, ToDoItem>().ReverseMap();
            
            CreateMap<DeleteToDoListCommand, ToDoList>().ReverseMap();
            CreateMap<DeleteToDoListCommand, ToDoList>().ReverseMap();
            CreateMap<GetListQueryResponse, ToDoList>().ReverseMap();
            CreateMap<GetAllListsQuery, List<GetAllListsQueryResponse>>().ReverseMap();
        }
    }
}
