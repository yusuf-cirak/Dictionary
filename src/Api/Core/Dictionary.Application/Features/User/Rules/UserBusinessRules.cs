using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Exceptions;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Features.Rules
{
    public sealed class UserBusinessRules
    {
        private readonly IUserRepository _repository;

        public UserBusinessRules(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> UserEmailMustExistAndConfirmedBeforeLogin(string email)
        {
            User? user = await _repository.GetSingleAsync(e=>e.Email==email);

            if (user is null) throw new BusinessException("Email or password not correct!");

            if (!user.EmailConfirmed) throw new BusinessException("Your email is not confirmed!");

                return user;
        }

        public void UserCredentialsMustMatchBeforeLogin(string passwordFromDb, string encryptedPassword)
        {
            if (passwordFromDb != encryptedPassword) throw new BusinessException("Email or password not correct!");
        }
    }
}
