using MRJ.DomainClasses;
using MRJ.ServiceLayer.Contracts;
using Microsoft.AspNet.Identity;

namespace MRJ.ServiceLayer
{
    public class CustomRoleStore : ICustomRoleStore
    {
        private readonly IRoleStore<CustomRole, int> _roleStore;

        public CustomRoleStore(IRoleStore<CustomRole, int> roleStore)
        {
            _roleStore = roleStore;
        }
    }
}