using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.EntryCommentFavorites.Commands.DeleteEntryCommentFavorite
{
    public sealed class DeleteEntryCommentFavoriteCommandRequest : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

    }
}
