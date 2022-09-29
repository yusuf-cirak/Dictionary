using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.DTOs
{
    public sealed class LoginUserCommandRequest:IRequest<LoginUserCommandResponse>
    {

        public string Email { get; }
        public string Password { get; }

        public LoginUserCommandRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public sealed class LoginUserCommandResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; } 
    }
}
