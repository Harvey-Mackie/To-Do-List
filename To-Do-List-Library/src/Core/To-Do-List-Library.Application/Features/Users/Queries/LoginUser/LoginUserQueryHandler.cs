using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Application.Contracts.Identity;
using To_Do_List_Library.Application.Models.Authentication;

namespace To_Do_List_Library.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public LoginUserQueryHandler(IMapper mapper, IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AuthenticationRequest>(request);
            return await _authenticationService.Authenticate(user);
        }
    }
}
