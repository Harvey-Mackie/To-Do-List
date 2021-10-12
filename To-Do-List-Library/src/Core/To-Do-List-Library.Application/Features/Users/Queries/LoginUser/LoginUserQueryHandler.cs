using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public LoginUserQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var loggedInUser = await _userRepository.LoginUser(user);

            return loggedInUser.UserId.ToString(); //Return Auth Token;
        }
    }
}
