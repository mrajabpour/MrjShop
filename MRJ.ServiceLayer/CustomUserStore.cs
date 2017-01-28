using MRJ.DataLayer;
using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MRJ.ServiceLayer
{
    public class CustomUserStore :
        UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        ICustomUserStore
    {
        //private readonly IDbSet<ApplicationUser> _myUserStore;
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
            //_myUserStore = context.Set<ApplicationUser>();
        }

        //public override Task<ApplicationUser> FindByIdAsync(int userId)
        //{
        //   return Task.FromResult(_myUserStore.Find(userId));
        //}
    }
}