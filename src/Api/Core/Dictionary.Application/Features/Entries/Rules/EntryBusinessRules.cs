using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Exceptions;
using Dictionary.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Dictionary.Application.Features.Entries.Rules
{
    public sealed class EntryBusinessRules
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public EntryBusinessRules(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task CheckUserCredentialBeforeEntryCreated(Guid userId)
        {
            string userIdFromToken =
                _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (userIdFromToken == null) throw new Exception("You are not authorized");

            Guid.TryParse(userIdFromToken, out Guid userGuid);

            User user = await _userRepository.GetByIdAsync(userId);

            if (user.Id != userGuid) throw new BusinessException("You are not authorized");
        }
    }
}
