using Dictionary.Common.Features.Entries.Commands.CreateEntry;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.EntryComments.Queries;
using Dictionary.Common.Pagination;
using Dictionary.WebApp.Infrastructure.Services.Abstractions;
using System.Net.Http.Json;

namespace Dictionary.WebApp.Infrastructure.Services.Concretes
{
    public sealed class EntryService : IEntryService
    {
        private readonly HttpClient _httpClient;

        public EntryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Entries
        public async Task<List<GetEntriesViewModel>> GetEntries(bool getTodayEntries = false, int count = 100)
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetEntriesViewModel>>($"api/entries?todayEntries={getTodayEntries}&count={count}");

            return result!;
        }

        public async Task<List<GetEntryDetailViewModel>> GetEntryDetail(Guid entryId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetEntryDetailViewModel>>($"api/entries/detail/{entryId}");

            return result!;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"api/entries/main?page={page}&pageSize={pageSize}");

            return result!;
        }

        public async Task<List<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await _httpClient.GetFromJsonAsync<List<GetEntryDetailViewModel>>($"api/entries/user?userName={userName}&page={page}&pageSize={pageSize}");

            return result!;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommandRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/entry", request);

            if (!response.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await response.Content.ReadAsStringAsync();
            return new Guid(guidStr.Trim('"'));
        }

        public async Task<SearchEntryViewModel> SearchEntryBySubject(string searchText)
        {
            var response = await _httpClient.GetFromJsonAsync<SearchEntryViewModel>($"api/entries/search?searchText={searchText}");

            return response!;
        }


        // EntryComments
        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryCommentsById(Guid entryId, int page, int pageSize)
        {
            var result = await _httpClient.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"api/entrycomments/{entryId}?page={page}&pageSize={pageSize}");

            return result!;
        }


        public async Task<Guid> CreateEntryComment(CreateEntryCommandRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/entrycomments", request);

            if (!result.IsSuccessStatusCode)
                return Guid.Empty;


            var guidStr = await result.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
    }
}
