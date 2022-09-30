using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.DTOs.Entry
{
    public sealed class DeleteEntryFavoriteCommandRequest:IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }

    }
}
