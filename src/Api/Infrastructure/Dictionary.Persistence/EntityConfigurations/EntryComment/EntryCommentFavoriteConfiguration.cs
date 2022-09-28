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
    public sealed class EntryCommentFavoriteConfiguration:BaseEntityConfiguration<EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.EntryComment)
                .WithMany(e => e.EntryCommentFavorites)
                .HasForeignKey(e => e.EntryCommentId);

            builder.HasOne(e => e.User)
                .WithOne().HasForeignKey<EntryCommentFavorite>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
