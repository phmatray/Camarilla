using System;
using System.Configuration;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Services
{
    public class AppSettingsService
    {
        public static string GetAdminFullname()
        {
            return ConfigurationManager.AppSettings["admin:Fullname"];
        }

        public static string GetAdminEmail()
        {
            return ConfigurationManager.AppSettings["admin:Email"];
        }

        public static string GetEmailServicePassword()
        {
            return ConfigurationManager.AppSettings["emailService:Password"];
        }

        public static string GetEmailServiceAccount()
        {
            return ConfigurationManager.AppSettings["emailService:Account"];
        }

        public static string GetAuthorizationServerAudienceName()
        {
            return ConfigurationManager.AppSettings["authorizationServer:AudienceName"];
        }

        public static string GetAuthorizationServerAudienceId()
        {
            return ConfigurationManager.AppSettings["authorizationServer:AudienceId"];
        }

        public static string GetAuthorizationServerAudienceSecret()
        {
            return ConfigurationManager.AppSettings["authorizationServer:AudienceSecret"];
        }

        public static User GetGod()
        {
            return new User
            {
                UserName = ConfigurationManager.AppSettings["god:username"],
                Email = ConfigurationManager.AppSettings["god:email"],
                EmailConfirmed = Convert.ToBoolean(ConfigurationManager.AppSettings["god:emailConfirmed"]),
                Birthday = new DateTime(
                    Convert.ToInt32(ConfigurationManager.AppSettings["god:birthday.year"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["god:birthday.month"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["god:birthday.day"])),
                FirstName = ConfigurationManager.AppSettings["god:firstName"],
                LastName = ConfigurationManager.AppSettings["god:lastName"],
                Gender = (Gender) Convert.ToInt32(ConfigurationManager.AppSettings["god:gender"])
            };
        }

        public static string GetGodPassword()
        {
            return ConfigurationManager.AppSettings["god:password"];
        }
    }
}