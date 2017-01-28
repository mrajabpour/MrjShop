using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MRJ.ServiceLayer
{
    public class ApplicationSignInManager :
        SignInManager<ApplicationUser, int>, IApplicationSignInManager
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        public ApplicationSignInManager(ApplicationUserManager userManager,
                                        IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }
    }
}