using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Persistence.EntityConfigurations
{
    public sealed class EntryConfiguration : BaseEntityConfiguration<Entry>
    {
        public override void Configure(EntityTypeBuilder<Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("Entries");


            builder.HasOne(e => e.User)
                .WithMany(e => e.Entries).HasForeignKey(e => e.UserId);


            builder.HasMany(e => e.EntryVotes)
                .WithOne(e => e.Entry).HasForeignKey(e => e.EntryId);


            builder.HasMany(e => e.EntryComments)
                .WithOne(e => e.Entry).HasForeignKey(e => e.EntryId);


            builder.HasMany(e => e.EntryFavorites)
                .WithOne(e => e.Entry).HasForeignKey(e => e.EntryId);

        }
    }
}
