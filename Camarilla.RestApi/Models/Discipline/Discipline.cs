using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Discipline : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Clan> Clans { get; set; } = new List<Clan>();
        public virtual ICollection<PersonaDiscipline> PersonaDisciplines { get; set; } = new List<PersonaDiscipline>();
    }
}