using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.Users.Commands.Update
{
    public class UpdateUserCommandRequest : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}