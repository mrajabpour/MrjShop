using Microsoft.AspNet.Identity.EntityFramework;

namespace MRJ.DomainClasses
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {

    }
}