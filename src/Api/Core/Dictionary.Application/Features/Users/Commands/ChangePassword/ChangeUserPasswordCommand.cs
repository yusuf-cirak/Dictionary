using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Users.Rules;
using Dictionary.Common.Features.Users.Commands.ChangePassword;
using Dictionary.Domain.Entities;
using MediatR;

namespace Dictionary.Application.Features.Users.Commands.ChangePassword
{
    public sealed class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommandRequest, bool>
    {
        private readonly UserBusinessRules _businessRules;
        private readonly IUserRepository _repository;

        public ChangeUserPasswordCommandHandler(UserBusinessRules businessRules, IUserRepository repository)
        {
            _businessRules = businessRules;
            _repository = repository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await _businessRules.CheckUserExistsBeforePasswordChanged(request.UserId);

            _businessRules.CurrentPasswordFromDbAndFromRequestMustMatchBeforePasswordChanged(request.CurrentPassword, user.Password); // Encrypt request.CurrentPassword and check if they match


            string newEncryptedPassword = _businessRules.CurrentPasswordAndNewPasswordCannotBeSameBeforePasswordChanged(user.Password,
                request.NewPassword); // Check if encrypted userPassword and new encrypted password match, if not return newEncrypted password

            user.Password = newEncryptedPassword;

           await _repository.UpdateAsync(user);

           return true;


        }
    }
}
