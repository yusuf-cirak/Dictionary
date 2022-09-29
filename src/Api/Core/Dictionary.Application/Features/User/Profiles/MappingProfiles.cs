using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Common.DTOs.User;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Features.Profiles
{
    public sealed class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, LoginUserCommandResponse>();
        }
    }
}
