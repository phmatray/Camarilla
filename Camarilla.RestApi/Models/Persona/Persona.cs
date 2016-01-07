using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Camarilla.RestApi.Models
{
    public class Persona : IModel
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Name { get; set; }
        public PersonaGender? PersonaGender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Background { get; set; }
        public int Generation { get; set; }
        public int ExperienceActual { get; set; }
        public int ExperienceRemaining { get; set; }
        public int Nights { get; set; }
        public int Willingness { get; set; }
        public int Humanity { get; set; }
        public string PictureUrl { get; set; }

        [ForeignKey(nameof(Clan))]
        public int? ClanId { get; set; }

        [ForeignKey(nameof(Race))]
        public int? RaceId { get; set; }

        [ForeignKey(nameof(Sire))]
        public int? SireId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual Race Race { get; set; }
        public virtual Persona Sire { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PersonaMail> Mails { get; set; } = new List<PersonaMail>();
        public virtual ICollection<Persona> Children { get; set; }  = new List<Persona>();

        [NotMapped]
        public List<PersonaMail> SentMails
            => Mails.Where(x => x.Deleted == null && x.Mail.FromPseudo == Pseudo).ToList();

        [NotMapped]
        public List<PersonaMail> ReceivedMails
            => Mails.Where(x => x.Deleted == null && x.Mail.ToPseudosList.Contains(Pseudo)).ToList();

        [NotMapped]
        public List<PersonaMail> DeletedMails
            => Mails.Where(x => x.Deleted != null).ToList();
    }
}