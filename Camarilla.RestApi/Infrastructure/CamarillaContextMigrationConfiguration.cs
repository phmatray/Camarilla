using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Camarilla.RestApi.Infrastructure.Services;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Infrastructure
{
    public class CamarillaContextMigrationConfiguration : DbMigrationsConfiguration<CamarillaContext>
    {
        public CamarillaContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            //AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CamarillaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var dbContext = CamarillaContext.Create();
            var manager = new UserManager<User>(new UserStore<User>(dbContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbContext));

            var god = AppSettingsService.God;
            god.JoinDate = DateTime.Now.AddYears(-3);

            manager.Create(god, AppSettingsService.GodPassword);

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole {Name = "God"});
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole {Name = "Hacker"});
                roleManager.Create(new IdentityRole {Name = "User"});
            }

            var adminUser = manager.FindByName("God");

            manager.AddToRoles(adminUser.Id, "God", "Admin", "Hacker", "User");


            if (!context.Races.Any())
            {
                context.Races.Add(new Race {Name = "Humain", Experience = 750, Description = "Un humain"});
                context.Races.Add(new Race {Name = "Goule", Experience = 1000, Description = "Une goule"});
                context.Races.Add(new Race {Name = "Nouveau-né", Experience = 1000, Description = "Un nouveau-né" });
                context.Races.Add(new Race {Name = "Vampire", Experience = 1250, Description = "Un vampire"});
                context.Races.Add(new Race {Name = "Vieux vampire", Experience = 1500, Description = "Un vieux vampire" });
                context.Races.Add(new Race {Name = "Ancien", Experience = 2000, Description = "Un ancien" });
                context.Races.Add(new Race {Name = "Primogène", Experience = 2000, Description = "Un primogène" });
                context.Races.Add(new Race {Name = "Antédiluvien", Experience = 6000, Description = "Un antédiluvien" });
            }

            if (!context.Clans.Any())
            {
                context.Clans.Add(new Clan { Name = "Humain", ClanCategory = ClanCategory.Human, ClanKind = ClanKind.Brujah, Description = "Humain" });
                context.Clans.Add(new Clan { Name = "Goule Brujah", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Brujah, Description = "Goule Brujah" });
                context.Clans.Add(new Clan { Name = "Goule Grangrel Rurale", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.RuralGangrel, Description = "Goule Grangrel Rurale" });
                context.Clans.Add(new Clan { Name = "Goule Grangrel Urbaine", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.UrbanGangrel, Description = "Goule Grangrel Urbaine" });
                context.Clans.Add(new Clan { Name = "Goule Malkavien", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Malkavien, Description = "Goule Malkavien" });
                context.Clans.Add(new Clan { Name = "Goule Nosferatu", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Nosferatu, Description = "Goule Nosferatu" });
                context.Clans.Add(new Clan { Name = "Goule Toréador", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Toreador, Description = "Goule Toréador" });
                context.Clans.Add(new Clan { Name = "Goule Tremere", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Tremere, Description = "Goule Tremere" });
                context.Clans.Add(new Clan { Name = "Goule Ventrue", ClanCategory = ClanCategory.Ghoul, ClanKind = ClanKind.Venture, Description = "Goule Ventrue" });
                context.Clans.Add(new Clan { Name = "Brujah", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Brujah, Description = "Brujah" });
                context.Clans.Add(new Clan { Name = "Grangrel Rurale", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.RuralGangrel, Description = "Grangrel Rurale" });
                context.Clans.Add(new Clan { Name = "Grangrel Urbaine", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.UrbanGangrel, Description = "Grangrel Urbaine" });
                context.Clans.Add(new Clan { Name = "Malkavien", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Malkavien, Description = "Malkavien" });
                context.Clans.Add(new Clan { Name = "Nosferatu", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Nosferatu, Description = "Nosferatu" });
                context.Clans.Add(new Clan { Name = "Toréador", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Toreador, Description = "Toréador" });
                context.Clans.Add(new Clan { Name = "Tremere", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Tremere, Description = "Tremere" });
                context.Clans.Add(new Clan { Name = "Ventrue", ClanCategory = ClanCategory.Vampire, ClanKind = ClanKind.Venture, Description = "Ventrue" });
            }
        }

        //        protected override void Seed(CamarillaContext context)

        //#if DEBUG
        //        {
        //            new CamarillaDataSeeder(context).Seed();
        //        }
        //#endif
    }
}