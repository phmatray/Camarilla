using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Db
{
    public class CamarillaContext : IdentityDbContext<User>
    {
        public CamarillaContext()
            : base("CamarillaContext", false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static CamarillaContext Create()
        {
            return new CamarillaContext();
        }
    }
}