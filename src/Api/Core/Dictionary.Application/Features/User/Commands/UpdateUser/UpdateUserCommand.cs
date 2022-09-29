using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Rules;
using Dictionary.Common;
using Dictionary.Common.DTOs;
using Dictionary.Common.Events;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.Commands.UpdateUser
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest,Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<Guid> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            User user =await _userBusinessRules.UserMustExistBeforeUpdated(request.Id);

            var email = user.Email;

            var isEmailChanged = string.CompareOrdinal(email, request.Email) != 0;
            // if isEmailChanged is not 0 than user.email and request.email are different

             _mapper.Map(request, user);

            var rows = _userRepository.UpdateAsync(user).Result;

            if (isEmailChanged && rows > 0)
            {
                var @event = new UserEmailChangedEvent { OldEmailAddress = null, NewEmailAddress = user.Email };

                QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.UserExchangeName,
                    exchangeType: DictionaryConstants.DefaultExchangeType,
                    DictionaryConstants.UserEmailChangedQueue,
                    obj: @event);

                user.EmailConfirmed = false;
                await _userRepository.UpdateAsync(user);
            }

            return user.Id;
        }
    }
}