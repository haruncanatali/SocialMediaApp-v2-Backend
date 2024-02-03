using Microsoft.AspNetCore.Identity;

namespace SHFT.Domain.Identity;

public class Role : IdentityRole<long>
{
    public Role() : base()
    {

    }

    public Role(string roleName) : base(roleName)
    {

    }
}