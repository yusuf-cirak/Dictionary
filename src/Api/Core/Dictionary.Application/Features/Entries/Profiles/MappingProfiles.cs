using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Common.Features.Entries.Commands.CreateEntry;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.EntryComments.Commands.Create;
using Dictionary.Common.Features.EntryFavorites.Commands.Create;
using Dictionary.Common.Features.EntryVotes.Commands.Create;
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


            CreateMap<Entry, GetEntriesViewModel>()
                .ForMember(evm => evm.CommentCount, e => e.MapFrom(e => e.EntryComments.Count));

        }
    }
}
