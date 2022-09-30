using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Common.Features.Users.Commands.ChangePassword;
using FluentValidation;

namespace Dictionary.Application.Features.Users.Commands.ChangePassword
{
    public sealed class ChangeUserPasswordValidator:AbstractValidator<ChangeUserPasswordCommandRequest>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(e => e.UserId).NotNull().WithMessage("{PropertyName} argument can not be null");
            RuleFor(e => e.NewPassword).NotNull().WithMessage("{PropertyName} argument can not be null");
            RuleFor(e => e.CurrentPassword).NotNull().WithMessage("{PropertyName} argument can not be null");
        }
    }
}
