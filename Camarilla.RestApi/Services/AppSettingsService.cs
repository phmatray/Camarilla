﻿using System;
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
    }
}