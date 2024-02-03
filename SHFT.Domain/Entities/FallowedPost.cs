using SHFT.Domain.Common;
using SHFT.Domain.Identity;

namespace SHFT.Domain.Entities;

public class FallowedPost : BaseEntity
{
    public long UserId { get; set; }
    public long PostId { get; set; }

    public User User { get; set; }
    public Post Post { get; set; }
}