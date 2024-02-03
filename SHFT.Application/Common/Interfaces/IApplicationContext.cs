using Hospital.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using SHFT.Domain.Entities;
using SHFT.Domain.Identity;

namespace SHFT.Application.Common.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<FallowedPost> FallowedPosts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}