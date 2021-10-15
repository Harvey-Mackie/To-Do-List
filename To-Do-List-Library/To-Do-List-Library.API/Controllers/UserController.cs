using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List_Library.Application.Features.Users.Commands.CreateUser;
using To_Do_List_Library.Application.Features.Users.Queries.LoginUser;

namespace To_Do_List_Library.API.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public Task<Guid> Register(CreateUserCommand createUserCommand)
        {
            return _mediator.Send(createUserCommand);
        }

        [HttpPost("Login")]
        public Task<string> Login(LoginUserQuery loginUserQuery)
        {
            return _mediator.Send(loginUserQuery);
        }
    }
}
