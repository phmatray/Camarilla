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
            //var userStore = new UserStore(context);
            //var userManager = new UserManager(userStore);

            //var user = new User("phmatray")
            //{
            //    Email = "phmatray@gmail.com",
            //    PhoneNumber = "0032473322929",
            //    Birthday = new DateTime(1988, 8, 1),
            //    FirstName = "Philippe",
            //    LastName = "Matray",
            //    Gender = Gender.Male
            //};

            //userManager.Create(user, "camarilla");

            base.Seed(context);
        }
    }
}