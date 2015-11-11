using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Clan : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
        public virtual ICollection<Persona> Personae { get; set; } = new List<Persona>();
        public ClanCategory ClanCategory { get; set; }
        public ClanKind ClanKind { get; set; }
    }
}