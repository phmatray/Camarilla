using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Skill : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SkillCategory Category { get; set; }
        public virtual ICollection<PersonaSkill> PersonaSkills { get; set; } = new List<PersonaSkill>();
    }
}