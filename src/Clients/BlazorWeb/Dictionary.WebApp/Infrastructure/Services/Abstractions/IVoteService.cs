namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    internal interface IVoteService
    {
        Task CreateEntryCommentDownVote(Guid entryCommentId);
        Task CreateEntryCommentUpVote(Guid entryCommentId);
        Task CreateEntryDownVote(Guid entryId);
        Task CreateEntryUpVote(Guid entryId);
        Task DeleteEntryCommentVoteAsync(Guid entryCommentId);
        Task DeleteEntryVoteAsync(Guid entryId);
    }
}