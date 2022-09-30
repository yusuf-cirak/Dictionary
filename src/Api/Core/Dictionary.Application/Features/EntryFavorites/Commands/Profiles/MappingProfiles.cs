using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Features.EntryFavorites.Commands.Profiles
{
    public sealed class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateEntryFavoriteCommandRequest, EntryFavorite>();

        }
    }
}
