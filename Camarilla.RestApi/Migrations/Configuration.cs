using System;
using System.Data.Entity.Migrations;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CamarillaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CamarillaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var manager = new UserManager<User>(new UserStore<User>(new CamarillaContext()));

            var user = new User
            {
                UserName = "SuperPowerUser",
                Email = "phmatray@gmail.com",
                EmailConfirmed = true,
                Birthday = new DateTime(1988, 8, 1),
                FirstName = "Philippe",
                LastName = "Matray",
                Gender = Gender.Male,
                JoinDate = DateTime.Now.AddYears(-3)
            };

            manager.Create(user, "MySuperP@ssword!");
        }
    }
}