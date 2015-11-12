using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Managers
{
    public class RoleManager : RoleManager<IdentityRole>, IManager
    {
        public RoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }
    }
}