using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dictionary.Common.Features.EntryComments.Commands.Create;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Features.EntryComments.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateEntryCommentCommandRequest, EntryComment>();

        }
    }
}
