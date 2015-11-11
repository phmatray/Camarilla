using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class CamarillaRoleStore : RoleStore<IdentityRole>
    {
        public CamarillaRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}