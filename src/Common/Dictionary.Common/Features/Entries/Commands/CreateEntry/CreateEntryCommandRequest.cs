using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dictionary.Common.Features.Entries.Commands.CreateEntry
{
    public sealed class CreateEntryCommandRequest : IRequest<Guid>
    {
        public Guid CreatedByUserId { get; }
        public string Subject { get; }
        public string Content { get; }

        public CreateEntryCommandRequest(Guid createdByUserId, string subject, string content)
        {
            CreatedByUserId = createdByUserId;
            Subject = subject;
            Content = content;
        }
    }
}
