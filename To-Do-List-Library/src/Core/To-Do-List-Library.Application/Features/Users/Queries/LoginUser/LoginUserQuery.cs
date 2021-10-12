using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace To_Do_List_Library.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
