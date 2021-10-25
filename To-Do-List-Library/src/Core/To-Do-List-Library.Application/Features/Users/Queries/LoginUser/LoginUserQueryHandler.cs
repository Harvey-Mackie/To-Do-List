using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using System.Threading;
using To_Do_List_Library.Core.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Application.Contracts.Identity;
using To_Do_List_Library.Core.Application.Models.Authentication;
using To_Do_List_Library.Core.Domain.Entities;

namespace To_Do_List_Library.Core.Application.Features.Users.Queries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;
        public LoginUserQueryHandler(IMapper mapper, IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var userRequest = _userRepository.EncryptPassword(_mapper.Map<User>(request));
            var user = _mapper.Map<AuthenticationRequest>(userRequest);
            return await _authenticationService.Authenticate(user);
        }
    }
}
