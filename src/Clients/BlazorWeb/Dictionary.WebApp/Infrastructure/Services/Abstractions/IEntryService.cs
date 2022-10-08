using Dictionary.Common.Features.Entries.Commands.CreateEntry;
using Dictionary.Common.Features.Entries.Models;
using Dictionary.Common.Features.EntryComments.Queries;
using Dictionary.Common.Pagination;

namespace Dictionary.WebApp.Infrastructure.Services.Abstractions
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommandRequest request);
        Task<Guid> CreateEntryComment(CreateEntryCommandRequest request);
        Task<List<GetEntriesViewModel>> GetEntries(bool getTodayEntries = false, int count = 100);
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryCommentsById(Guid entryId, int page, int pageSize);
        Task<List<GetEntryDetailViewModel>> GetEntryDetail(Guid entryId);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<List<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);
        Task<SearchEntryViewModel> SearchEntryBySubject(string searchText);
    }
}