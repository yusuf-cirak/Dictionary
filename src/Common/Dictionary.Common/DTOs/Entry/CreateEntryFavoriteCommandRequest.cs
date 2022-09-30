using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.DTOs.Entry
{
    public sealed class CreateEntryFavoriteCommandRequest:IRequest<bool>
    {
        public Guid UserId { get;}
        public Guid EntryId { get;}

        public CreateEntryFavoriteCommandRequest(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}
