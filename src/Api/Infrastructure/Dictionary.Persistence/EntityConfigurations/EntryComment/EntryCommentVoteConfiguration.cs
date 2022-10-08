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
    public sealed class EntryCommentVoteConfiguration : BaseEntityConfiguration<EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.EntryComment)
                .WithMany(e => e.EntryCommentVotes).HasForeignKey(e => e.EntryCommentId);

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<EntryCommentVote>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
