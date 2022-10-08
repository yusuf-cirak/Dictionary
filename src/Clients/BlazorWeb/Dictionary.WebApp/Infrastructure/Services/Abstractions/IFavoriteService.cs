namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    public interface IFavoriteService
    {
        Task CreateEntryCommentFavorite(Guid entryCommentId);
        Task CreateEntryFavorite(Guid entryId);
        Task DeleteEntryCommentFavorite(Guid entryCommentId);
        Task DeleteEntryFavorite(Guid entryId);
    }
}