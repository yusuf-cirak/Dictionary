using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Common.DTOs.Entry;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Features.Entries.Profiles
{
    public sealed class EntryMappingProfiles:Profile
    {
        public EntryMappingProfiles()
        {
            CreateMap<CreateEntryCommandRequest, Entry>()
                .ForMember(e => e.UserId,
                    r => r.MapFrom(r => r.CreatedByUserId));


            CreateMap<CreateEntryFavoriteCommandRequest, EntryFavorite>();

            CreateMap<CreateEntryVoteCommandRequest, EntryVote>();

            CreateMap<CreateEntryCommentCommandRequest, EntryComment>();


        }
    }
}
