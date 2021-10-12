using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using To_Do_List_Library.Application.Contracts.Persistence;
using To_Do_List_Library.Core.Entities;

namespace To_Do_List_Library.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            var userAdded = await _userRepository.AddAsync(user);
            return userAdded.UserId;
        }
    }
}
