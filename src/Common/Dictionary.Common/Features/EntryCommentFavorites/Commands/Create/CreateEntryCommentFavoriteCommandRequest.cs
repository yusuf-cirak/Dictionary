using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Features.EntryCommentFavorites.Commands.Create
{
    public sealed class CreateEntryCommentFavoriteCommandRequest : IRequest<bool>
    {
        public Guid EntryCommentId { get; }
        public Guid UserId { get; }

        public CreateEntryCommentFavoriteCommandRequest(Guid entryCommentId, Guid userId)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
        }
    }
}
