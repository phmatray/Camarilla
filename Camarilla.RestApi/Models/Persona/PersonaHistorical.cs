using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaHistorical : IModel
    {
        [Key, Column(Order = 0)]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        public int HistoricalId { get; set; }

        public HistoricalLevel HistoricalLevel { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual Historical Historical { get; set; }
    }
}