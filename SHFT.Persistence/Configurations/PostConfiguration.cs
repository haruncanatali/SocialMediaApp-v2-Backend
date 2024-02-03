using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SHFT.Domain.Entities;

namespace SHFT.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Content).IsRequired();
        builder.Property(c => c.UserId).IsRequired();
    }
}