﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.EntryComments.Commands.Create
{
    public sealed class CreateEntryCommentCommandRequest : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid EntryId { get; set; }
        public string Content { get; set; }
    }
}
