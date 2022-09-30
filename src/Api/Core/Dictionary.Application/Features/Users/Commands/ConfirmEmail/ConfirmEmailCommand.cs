using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Users.Rules;

namespace Dictionary.Application.Features.Users.Commands.ConfirmEmail
{
    public sealed class ConfirmUserEmailCommandRequest : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }

    public sealed class ConfirmUserEmailCommandHandler : IRequestHandler<ConfirmUserEmailCommandRequest,bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserBusinessRules _businessRules;


        public ConfirmUserEmailCommandHandler(IUserRepository userRepository, UserBusinessRules businessRules)
        {
            _userRepository = userRepository;
            _businessRules = businessRules;
        }

        public async Task<bool> Handle(ConfirmUserEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var confirmation=await _businessRules.EmailConfirmationMustExistBeforeConfirmed(request.ConfirmationId);


            var user = await _businessRules.EmailMustBeRegisteredToUserBeforeConfirmed(confirmation.NewEmailAddress);

            user.EmailConfirmed = true;

            await _userRepository.UpdateAsync(user);

            return true;

        }
    }
}
