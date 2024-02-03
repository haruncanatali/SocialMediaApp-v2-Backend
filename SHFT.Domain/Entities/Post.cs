using SHFT.Domain.Common;
using SHFT.Domain.Identity;

namespace SHFT.Domain.Entities;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
    public List<FallowedPost> FallowedPosts { get; set; }
}