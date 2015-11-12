using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Managers
{
    internal class UserManager : UserManager<User>, IManager
    {
        public UserManager(UserStore store)
            : base(store)
        {
        }

        public void SaveAll()
        {
            var userStore = Store as UserStore;

            userStore?.SaveAll();
        }
    }
}