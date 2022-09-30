using MediatR;

namespace Dictionary.Common.DTOs.Entry;

public sealed class DeleteEntryVoteCommandRequest : IRequest<bool>
{
    public Guid UserId { get; }
    public Guid EntryId { get; }

    public DeleteEntryVoteCommandRequest(Guid userId, Guid entryId)
    {
        UserId = userId;
        EntryId = entryId;
    }
}