using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Persistence.EntityConfigurations
{
    public sealed class EntryFavoriteConfiguration:BaseEntityConfiguration<EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.Entry)
                .WithMany(e => e.EntryFavorites).HasForeignKey(e => e.EntryId);

            builder.HasOne(e => e.User)
                .WithMany(e=>e.EntryFavorites).HasForeignKey(e => e.UserId);

        }
    }
}
