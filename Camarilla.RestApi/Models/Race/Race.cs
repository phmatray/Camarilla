using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Race : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Experience { get; set; } = 0;
        public virtual ICollection<Persona> Personae { get; set; } = new List<Persona>();
    }
}