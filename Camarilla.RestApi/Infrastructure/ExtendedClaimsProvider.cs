using System;
using System.Collections.Generic;
using System.Security.Claims;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Infrastructure
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(User user)
        {
            List<Claim> claims = new List<Claim>();

            var daysInGame = user.JoinDate.HasValue
                ? (DateTime.Now.Date - user.JoinDate.Value).TotalDays
                : 0;

            if (daysInGame > 90)
            {
                claims.Add(CreateClaim("Novice", "1"));
            }
            else
            {
                claims.Add(CreateClaim("Novice", "0"));
            }

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
    }
}