using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaMail : IModel
    {
        [Key, Column(Order = 0)]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        public int MailId { get; set; }

        public DateTime? Read { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Persona Persona { get; set; } // connectedPersona
        public virtual Mail Mail { get; set; }
    }
}