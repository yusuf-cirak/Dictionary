using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Persistence.EntityConfigurations
{
    public sealed class UserConfiguration:BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);


            builder.HasMany(e => e.EntryComments)
                .WithOne(e => e.User).HasForeignKey(e => e.UserId);

            builder.HasMany(e => e.EntryCommentFavorites)
                .WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}
