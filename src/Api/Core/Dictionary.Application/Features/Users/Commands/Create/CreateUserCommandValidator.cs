using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.DTOs;
using FluentValidation;

namespace Dictionary.Application.Features.Users.Commands.Create
{
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.UserName).NotNull();
            RuleFor(e => e.Email).NotNull().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid e-mail address!");

            RuleFor(e => e.Password).NotNull().MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLength} character");
        }
    }
}
