using Camarilla.RestApi.Db;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Camarilla.RestApi.Managers
{
    public class UserManager : UserManager<User>, IManager
    {
        public UserManager(UserStore<User> store)
            : base(store)
        {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<CamarillaContext>();
            var userManager = new UserManager(new UserStore<User>(dbContext));

            return userManager;
        }
    }
}