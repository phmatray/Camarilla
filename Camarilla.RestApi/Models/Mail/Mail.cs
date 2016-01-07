using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        /// <summary>
        ///     The sender's pseudo
        /// </summary>
        public string FromPseudo { get; set; }

        /// <summary>
        ///     The pseudos of the receivers separated by a semicolon
        /// </summary>
        public string ToPseudos { get; set; }

        /// <summary>
        ///     Association table
        /// </summary>
        public virtual ICollection<PersonaMail> Mailboxes { get; set; } = new List<PersonaMail>();

        [NotMapped]
        public List<string> ToPseudosList
            => ToPseudos.Split(';').ToList();
    }
}