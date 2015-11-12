using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(DbContext context)
            : base(context)
        {
        }
    }
}