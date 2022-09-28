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
    public class EntryVoteConfiguration:BaseEntityConfiguration<EntryVote>
    {
        public override void Configure(EntityTypeBuilder<EntryVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("EntryVotes");

            builder.Property(e => e.VoteType).HasDefaultValue(0);

            builder.HasOne(e => e.Entry)
                .WithMany(e => e.EntryVotes).HasForeignKey(e => e.EntryId);

            builder.HasOne(e => e.User)
                .WithOne().HasForeignKey<EntryVote>(e => e.UserId);

        }
    }
}
