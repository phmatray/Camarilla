using System;
using Camarilla.RestApi.Infrastructure.Services;
using Camarilla.RestApi.Infrastructure.Stores.Concretes;
using Camarilla.RestApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Camarilla.RestApi.Infrastructure.Managers
{
    public class ApplicationUserManager : UserManager<User>, IManager
    {
        public ApplicationUserManager(UserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<CamarillaContext>();
            var userManager = new ApplicationUserManager(new ApplicationUserStore<User>(dbContext));

            userManager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(24)
                };
            }

            //Configure validation logic for usernames
            userManager.UserValidator = new UserValidator<User>(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            //Configure validation logic for passwords
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false
            };

            return userManager;
        }
    }
}