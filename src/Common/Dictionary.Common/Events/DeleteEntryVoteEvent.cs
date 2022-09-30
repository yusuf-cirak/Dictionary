namespace Dictionary.Common.Events;

public sealed class DeleteEntryVoteEvent
{
    public Guid UserId { get; set; }
    public Guid EntryId { get; set; }
}