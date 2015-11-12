using System.Data.Entity;
using System.Linq;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Db
{
    public class CamarillaContext : IdentityDbContext<User>
    {
        static CamarillaContext()
        {
            //var dbInitializer = new DropCreateDatabaseAlways<CamarillaContext>();
            //Database.SetInitializer(dbInitializer);
        }

        //public IQueryable<UserInformation> UserInformations { get; set; }
        //{

        public CamarillaContext() : base("CamarillaContext")
        {
        }
    }
}