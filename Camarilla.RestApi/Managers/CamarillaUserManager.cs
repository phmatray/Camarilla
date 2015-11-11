using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Managers
{
    internal class CamarillaUserManager : UserManager<CamarillaUser>
    {
        public CamarillaUserManager(CamarillaUserStore store)
            : base(store)
        {
        }

        public void SaveAll()
        {
            var userStore = Store as CamarillaUserStore;

            userStore?.SaveAll();
        }
    }
}