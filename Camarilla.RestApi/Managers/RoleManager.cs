using Camarilla.RestApi.Db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Camarilla.RestApi.Managers
{
    public class RoleManager : RoleManager<IdentityRole>, IManager
    {
        public RoleManager(IRoleStore<IdentityRole, string> store)
            : base(store)
        {
        }

        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context)
        {
            var appRoleManager = new RoleManager(new RoleStore<IdentityRole>(context.Get<CamarillaContext>()));

            return appRoleManager;
        }
    }
}