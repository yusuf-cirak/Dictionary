﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Domain.Entities;
using Dictionary.Persistence.Context;

namespace Dictionary.Persistence.Repositories
{
    public sealed class EntryCommentFavoriteRepository:Repository<EntryCommentFavorite,DictionaryContext>,IEntryCommentFavoriteRepository
    {
        public EntryCommentFavoriteRepository(DictionaryContext context) : base(context)
        {
        }
    }
}
