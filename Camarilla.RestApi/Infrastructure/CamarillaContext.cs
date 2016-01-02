using System.Data.Entity;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Infrastructure
{
    public class CamarillaContext : IdentityDbContext<User>
    {
        public CamarillaContext()
            : base("CamarillaContext", false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CamarillaContext, CamarillaContextMigrationConfiguration>());
        }

        public DbSet<Race> Races { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Persona> Personae { get; set; }
        public DbSet<Mailbox> Mailboxes { get; set; }
        public DbSet<Mail> Mails { get; set; }

        public static CamarillaContext Create()
        {
            return new CamarillaContext();
        }
    }
}