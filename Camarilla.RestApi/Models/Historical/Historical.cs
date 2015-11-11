using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Historical : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<PersonaHistorical> PersonaHistoricals { get; set; } = new List<PersonaHistorical>();
    }
}