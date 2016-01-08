using System;
using System.Configuration;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure.Services
{
    public class AppSettingsService
    {
        public static string AdminFullname
            => ConfigurationManager.AppSettings["admin:Fullname"];

        public static string AdminEmail
            => ConfigurationManager.AppSettings["admin:Email"];

        public static string NoReplyFullname
            => ConfigurationManager.AppSettings["noreply:Fullname"];

        public static string NoReplyEmail
            => ConfigurationManager.AppSettings["noreply:Email"];

        public static string EmailServicePassword
            => ConfigurationManager.AppSettings["emailService:Password"];

        public static string EmailServiceAccount
            => ConfigurationManager.AppSettings["emailService:Account"];

        public static string AuthorizationServerAudienceName
            => ConfigurationManager.AppSettings["authorizationServer:AudienceName"];

        public static string AuthorizationServerAudienceId
            => ConfigurationManager.AppSettings["authorizationServer:AudienceId"];

        public static string AuthorizationServerAudienceSecret
            => ConfigurationManager.AppSettings["authorizationServer:AudienceSecret"];

        public static User God => new User
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

        public static string GodPassword
            => ConfigurationManager.AppSettings["god:password"];
    }
}