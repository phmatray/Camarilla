using System.Data.Entity;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Stores.Concretes
{
    public class CamarillaUserStore : UserStore<CamarillaUser>
    {
        public CamarillaUserStore(DbContext context)
            : base(context)
        {
            AutoSaveChanges = false;
        }

        public void SaveAll()
        {
            Context.SaveChanges();
        }
    }
}