using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaSkill : IModel
    {
        [Key, Column(Order = 0)]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        public int SkillId { get; set; }

        public SkillLevel SkillLevel { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Skill Skill { get; set; }
    }
}