using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Camarilla.RestApi.Services
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
    }
}