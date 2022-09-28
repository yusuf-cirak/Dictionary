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
    public sealed class EntryCommentConfiguration:BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);


            builder.HasOne(e => e.User)
                .WithMany(e => e.EntryComments)
                .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Entry)
                .WithMany(e => e.EntryComments).HasForeignKey(e => e.EntryId);

            builder.HasMany(e => e.EntryCommentVotes)
                .WithOne(e => e.EntryComment)
                .HasForeignKey(e => e.EntryCommentId);

            builder.HasMany(e => e.EntryCommentFavorites)
                .WithOne(e => e.EntryComment)
                .HasForeignKey(e => e.EntryCommentId);
        }
    }
}
