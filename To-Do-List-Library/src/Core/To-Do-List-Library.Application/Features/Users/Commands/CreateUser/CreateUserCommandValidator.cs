using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using To_Do_List_Library.Core.Application.Contracts.Persistence;

namespace To_Do_List_Library.Core.Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(e => e.Email)
                .Must(email => UserEmailUnique(email, CancellationToken.None))
                .WithMessage("Email address is not unique - already taken");
        }

        private bool UserEmailUnique(string email, CancellationToken cancellationToken)
        {
            return _userRepository.IsUserEmailUnique(email);
        }
    }
}
