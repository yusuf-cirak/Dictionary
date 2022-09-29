using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Domain.Entities;
using Dictionary.Persistence.Context;

namespace Dictionary.Persistence.Repositories
{
    public class EntryCommentVoteRepository:Repository<EntryCommentVote,DictionaryContext>,IEntryCommentVoteRepository
    {
        public EntryCommentVoteRepository(DictionaryContext context) : base(context)
        {
        }
    }
}
