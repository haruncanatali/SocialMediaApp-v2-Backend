using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using SHFT.Domain.Entities;
using SHFT.Domain.Enums;

namespace SHFT.Domain.Identity;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public string ProfilePhoto { get; set; }
    public DateTime Birthdate { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long? DeletedBy { get; set; }
    public long? UpdatedBy { get; set; }

    public List<FallowedPost> FallowedPosts { get; set; }

    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
}