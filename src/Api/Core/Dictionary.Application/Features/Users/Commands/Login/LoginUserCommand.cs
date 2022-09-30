using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Application.Features.Users.Rules;
using Dictionary.Common.DTOs;
using Dictionary.Common.Infrastructure;
using Dictionary.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Dictionary.Application.Features.Users.Commands.Login
{
    public sealed class LoginUserCommandHandler:IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _businessRules;

        public LoginUserCommandHandler(UserBusinessRules businessRules, IMapper mapper)
        {
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await _businessRules.UserEmailMustExistAndConfirmedBeforeLogin(request.Email);

            var encryptedPassword = PasswordEncryptor.Encrypt(request.Password);

            _businessRules.UserCredentialsMustMatchBeforeLogin(user.Password, encryptedPassword);


            var result = _mapper.Map<LoginUserCommandResponse>(user);

            result.AccessToken = GenerateToken(SetDefaultClaims(user));

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecretkey"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(claims:claims,
                expires:expire,
                signingCredentials:credentials,
                notBefore:DateTime.Now);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private Claim[] SetDefaultClaims(User user)
        {
            return new Claim[]
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),

            };
        }
    }
}
