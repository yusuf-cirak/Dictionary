using Dictionary.Common.Enums;
using Dictionary.WebApp.Infrastructure.Services.Abstractions;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    internal sealed class VoteService : IVoteService
    {
        private readonly HttpClient _httpClient;

        public VoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region EntryVote

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType)
        {
            // second parameter is HttpContent which you can send your body data
            var result = await _httpClient.PostAsync($"/api/votes/entry/{entryId}?voteType={voteType}", null);

            if (!result.IsSuccessStatusCode) throw new Exception("Something went wrong");

            return result;

        }


        public async Task DeleteEntryVoteAsync(Guid entryId)
        {

            await _httpClient.DeleteAsync($"/api/votes/entry-vote/{entryId}");
        }

        #endregion


        #region EntryCommentVote
        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType)
        {
            // second parameter is HttpContent which you can send your body data
            var result = await _httpClient.PostAsync($"/api/votes/entry-comment/{entryCommentId}?voteType={voteType}", null);

            if (!result.IsSuccessStatusCode) throw new Exception("Something went wrong");

            return result;

        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }

        public async Task DeleteEntryCommentVoteAsync(Guid entryCommentId)
        {

            await _httpClient.DeleteAsync($"/api/votes/entry-comment-vote/{entryCommentId}");
        }


        #endregion

    }
}
