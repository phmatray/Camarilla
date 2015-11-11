using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Camarilla.RestApi.Models
{
    public class Mail : IModel
    {
        public int Id { get; set; }

        [DisplayName("Objet")]
        public string Subject { get; set; } = string.Empty;

        [DisplayName("Message")]
        public string Message { get; set; } = string.Empty;

        public DateTime Sent { get; set; }
        public virtual Persona From { get; set; }
        public virtual ICollection<Persona> To { get; set; } = new List<Persona>();

        public virtual ICollection<PersonaMail> ConnectedPersonae { get; set; } = new List<PersonaMail>();
        // sender and receivers informations
    }
}