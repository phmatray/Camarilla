﻿using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Persona : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Background { get; set; }
        public int Generation { get; set; }
        public int ExperienceActual { get; set; }
        public int ExperienceRemaining { get; set; }
        public int Nights { get; set; }
        public int Willingness { get; set; }
        public int Humanity { get; set; }
        public virtual Clan Clan { get; set; }
        public virtual Race Race { get; set; }
        public virtual Persona Sire { get; set; }
        public virtual CamarillaUser CamarillaUser { get; set; }
        public virtual ICollection<PersonaMail> LetterBox { get; set; } = new List<PersonaMail>();
    }
}