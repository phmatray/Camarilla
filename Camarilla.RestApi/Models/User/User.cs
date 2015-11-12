using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}