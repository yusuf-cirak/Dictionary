using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.EntryFavorites.Commands.Delete
{
    public sealed class DeleteEntryFavoriteCommandRequest : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }

    }
}
