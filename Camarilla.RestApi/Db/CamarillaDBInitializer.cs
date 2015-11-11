using System.Data.Entity;

namespace Camarilla.RestApi.Db
{
    public class CamarillaDBInitializer : DropCreateDatabaseAlways<CamarillaContext>
    {
        protected override void Seed(CamarillaContext context)
        {
            //var userManager = new CamarillaUserManager(new CamarillaUserStore(context));

            //var userInformation = new UserInformation
            //{
            //    Birthday = new DateTime(1988, 8, 1),
            //    FirstName = "Philippe",
            //    LastName = "Matray",
            //    Gender = Gender.Male
            //};

            //var camarillaUser = new CamarillaUser("phmatray")
            //{
            //    Email = "phmatray@gmail.com",
            //    PhoneNumber = "0032473322929",
            //    UserInformation = userInformation
            //};

            //userManager.Create(camarillaUser, "camarilla");

            //base.Seed(context);
        }
    }
}