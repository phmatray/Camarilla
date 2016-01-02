using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class Mailbox : IModel
    {
        [Key, ForeignKey("MailboxOf")]
        public int Id { get; set; }

        public virtual Persona MailboxOf { get; set; }
        public virtual ICollection<PersonaMail> Mails { get; set; } = new List<PersonaMail>();
    }
}