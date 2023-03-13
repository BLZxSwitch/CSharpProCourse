using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tickets.Core.UserAggregate;

namespace Tickets.Infrastructure.Data.Config;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        // builder.Property(user => user.Id).HasDefaultValueSql("newsequentialid()");            
        builder.Property(user => user.IsActive).IsRequired().HasDefaultValue(false);
        builder.ToTable("User", "auth");
    }
}
