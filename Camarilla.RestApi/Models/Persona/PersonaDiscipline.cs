using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaDiscipline : IModel
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Persona))]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Discipline))]
        public int DisciplineId { get; set; }

        public bool Demonized { get; set; }
        public DisciplineLevel Level { get; set; }

        [ForeignKey(nameof(Clan))]
        public int ClanId { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}