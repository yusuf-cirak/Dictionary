using Dictionary.WebApp.Infrastructure.Services.Abstractions;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    public sealed class FavoriteService : IFavoriteService
    {
        private readonly HttpClient _httpClient;

        public FavoriteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEntryFavorite(Guid entryId)
        {
            await _httpClient.PostAsync($"api/entryfavorites/entry/{entryId}", null);
        }

        public async Task DeleteEntryFavorite(Guid entryId)
        {
            await _httpClient.DeleteAsync($"api/entryfavorites/entry/{entryId}");
        }

        public async Task CreateEntryCommentFavorite(Guid entryCommentId)
        {
            await _httpClient.PostAsync($"api/entryfavorites/comment/{entryCommentId}", null);
        }

        public async Task DeleteEntryCommentFavorite(Guid entryCommentId)
        {
            await _httpClient.DeleteAsync($"api/entryfavorites/comment/{entryCommentId}");
        }
    }
}
