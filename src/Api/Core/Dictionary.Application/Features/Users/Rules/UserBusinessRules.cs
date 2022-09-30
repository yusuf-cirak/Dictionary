using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Exceptions;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using FluentValidation.Validators;
using MediatR;

namespace Dictionary.Application.Features.Users.Rules
{
    public sealed class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailConfirmationRepository _emailConfirmationRepository;

        public UserBusinessRules(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            _userRepository = userRepository;
            _emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<User> UserEmailMustExistAndConfirmedBeforeLogin(string email)
        {
            User? user = await _userRepository.GetSingleAsync(e => e.Email == email);

            if (user is null) throw new BusinessException("Email or password not correct!");

            if (!user.EmailConfirmed) throw new BusinessException("Your email is not confirmed!");

            return user;
        }

        public void UserCredentialsMustMatchBeforeLogin(string passwordFromDb, string encryptedPassword)
        {
            if (passwordFromDb != encryptedPassword) throw new BusinessException("Email or password not correct!");
        }

        public async Task UserEmailCannotBeDuplicatedBeforeRegistered(string email)
        {
            if (await _userRepository.FirstOrDefaultAsync(e => e.Email == email) != null)
            {
                throw new BusinessException("A user already exists with that email");
            }
        }

        public async Task<User> UserMustExistBeforeUpdated(Guid id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new BusinessException("User does not exist");

            return user;

        }

        public async Task<User> CheckUserExistsBeforePasswordChanged(Guid userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);

            if (user is null) throw new BusinessException("User not found");

            return user;
        }

        public void CurrentPasswordFromDbAndFromRequestMustMatchBeforePasswordChanged(string currentPasswordFromRequest, string userPasswordFromDb)
        {
            currentPasswordFromRequest = PasswordEncryptor.Encrypt(currentPasswordFromRequest);

            if (currentPasswordFromRequest != userPasswordFromDb) throw new BusinessException("Your old password credential is wrong");
        }

        public string CurrentPasswordAndNewPasswordCannotBeSameBeforePasswordChanged(string currentPasswordFromDb, string newPasswordFromRequest)
        {
            newPasswordFromRequest = PasswordEncryptor.Encrypt(newPasswordFromRequest);

            if (currentPasswordFromDb == newPasswordFromRequest) throw new BusinessException("Current password and new password can not be the same");

            return newPasswordFromRequest;
        }

        public async Task<EmailConfirmation> EmailConfirmationMustExistBeforeConfirmed(Guid confirmationId)
        {
            EmailConfirmation emailConfiguration = await _emailConfirmationRepository.GetByIdAsync(confirmationId);

            if (emailConfiguration is null) throw new BusinessException("Confirmation not found!");

            return emailConfiguration;
        }

        public async Task<User> EmailMustBeRegisteredToUserBeforeConfirmed(string email)
        {
            User? user = await _userRepository.FirstOrDefaultAsync(e => e.Email == email);

            if (user is null) throw new BusinessException("No user registered with that email!");

            if (user.EmailConfirmed) throw new BusinessException("Email is already confirmed");
                return user;
        }
    }
}
