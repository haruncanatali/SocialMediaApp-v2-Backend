using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHFT.Domain.Entities;

namespace SHFT.Persistence.Configurations;

public class FallowedPostConfiguration : IEntityTypeConfiguration<FallowedPost>
{
    public void Configure(EntityTypeBuilder<FallowedPost> builder)
    {
        builder.Property(c => c.PostId).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
    }
}