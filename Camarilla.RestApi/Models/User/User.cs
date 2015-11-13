using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Models
{
    public class User : IdentityUser, IModel
    {
        [DisplayName("Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Nom")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Date de naissance")]
        public DateTime? Birthday { get; set; }

        [DisplayName("Date d'inscription")]
        public DateTime? JoinDate { get; set; }

        [DisplayName("Genre")]
        public Gender Gender { get; set; }

        public virtual ICollection<Persona> Personae { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}