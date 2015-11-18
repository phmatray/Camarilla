using System.Data.Entity.Migrations;

namespace Camarilla.RestApi.Infrastructure
{
    public class CamarillaContextMigrationConfiguration : DbMigrationsConfiguration<CamarillaContext>
    {
        public CamarillaContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

//#if DEBUG
//        protected override void Seed(CamarillaContext context)
//        {
//            new CamarillaDataSeeder(context).Seed();
//        }
//#endif
    }
}