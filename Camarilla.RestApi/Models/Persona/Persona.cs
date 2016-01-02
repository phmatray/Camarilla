using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual Mailbox Mailbox { get; set; }

        public virtual ICollection<Mail> Sended { get; set; } = new List<Mail>();
        public virtual ICollection<Persona> Children { get; set; }  = new List<Persona>();
    }
}