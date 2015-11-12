using System;
using System.Data.Entity.Migrations;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Models;

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

            var user = new User("phmatray")
            {
                Email = "phmatray@gmail.com",
                PhoneNumber = "0032473322929",
                Birthday = new DateTime(1988, 8, 1),
                FirstName = "Philippe",
                LastName = "Matray",
                Gender = Gender.Male,
                JoinDate = DateTime.Now
            };

            context.Users.AddOrUpdate(u => u.Email, user);
            context.SaveChanges();
        }
    }
}