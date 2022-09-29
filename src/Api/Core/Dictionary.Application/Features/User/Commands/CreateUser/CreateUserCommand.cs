using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Dictionary.Application.Features.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler:IRequestHandler<CreateUserCommandRequest,Guid>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _businessRules;

        public CreateUserCommandHandler(IUserRepository repository, IMapper mapper, UserBusinessRules businessRules)
        {
            _repository = repository;
            _mapper = mapper;
            _businessRules = businessRules;
        }


        public async Task<Guid> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           await _businessRules.UserEmailCannotBeDuplicatedBeforeRegistered(request.Email);

           var user =_mapper.Map<User>(request);

          var rows= await _repository.AddAsync(user);

           // Email changed/created

           if (rows>0)
           {
               var @event = new UserEmailChangedEvent { OldEmailAddress = null, NewEmailAddress = user.Email };

               QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.UserExchangeName,
                   exchangeType: DictionaryConstants.DefaultExchangeType,
                   DictionaryConstants.UserEmailChangedQueue,
                   obj: @event);
           }


           return user.Id;

        }
    }
}
