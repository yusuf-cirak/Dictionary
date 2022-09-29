using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Domain.Entities;
using Dictionary.Persistence.Context;

namespace Dictionary.Persistence.Repositories
{
    public sealed class EntryRepository : Repository<Entry, DictionaryContext>, IEntryRepository
    {
        public EntryRepository(DictionaryContext context) : base(context)
        {
        }
    }
}
