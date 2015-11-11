using System;
using System.ComponentModel;

namespace Camarilla.RestApi.Models
{
    public class UserInformation : IModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Prénom")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Nom")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Date de naissance")]
        public DateTime Birthday { get; set; }

        [DisplayName("Genre")]
        public Gender Gender { get; set; }

        [DisplayName("Utilisateur")]
        public virtual CamarillaUser CamarillaUser { get; set; }
    }
}