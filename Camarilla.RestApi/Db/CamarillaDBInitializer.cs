using System;
using System.Data.Entity;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Db
{
    public class CamarillaDbInitializer : DropCreateDatabaseAlways<CamarillaContext>
    {
        protected override void Seed(CamarillaContext context)
        {
            var userStore = new CamarillaUserStore(context);
            var userManager = new CamarillaUserManager(userStore);

            var userInformation = new UserInformation
            {
                Birthday = new DateTime(1988, 8, 1),
                FirstName = "Philippe",
                LastName = "Matray",
                Gender = Gender.Male
            };

            var camarillaUser = new CamarillaUser("phmatray")
            {
                Email = "phmatray@gmail.com",
                PhoneNumber = "0032473322929",
                UserInformation = userInformation
            };

            userManager.Create(camarillaUser, "camarilla");

            base.Seed(context);
        }
    }
}