using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Identity;

public class UserRole : IdentityUserRole<long>
{
    public UserRole() : base()
    {
            
    }
}