using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities;
using Dictionary.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Persistence.Context
{
    public sealed class DictionaryContext:DbContext
    {
        public DictionaryContext(DbContextOptions optins):base(optins)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryVote> EntryVotes { get; set; }

        public DbSet<EntryComment> EntryComments { get; set; }
        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch //
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.Now,
                    _ => DateTime.Now //
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
