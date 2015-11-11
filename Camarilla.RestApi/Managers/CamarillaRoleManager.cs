using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Managers
{
    public class CamarillaRoleManager : RoleManager<IdentityRole>
    {
        public CamarillaRoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }
    }
}