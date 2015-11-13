using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Services;
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

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CamarillaContext()));

            var god = AppSettingsService.GetGod();
            god.JoinDate = DateTime.Now.AddYears(-3);

            manager.Create(god, AppSettingsService.GetGodPassword());

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "God" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Hacker" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("God");

            manager.AddToRoles(adminUser.Id, new string[] { "God", "Admin", "Hacker" });
        }
    }
}