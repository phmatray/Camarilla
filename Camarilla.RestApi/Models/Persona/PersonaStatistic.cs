using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaStatistic : IModel
    {
        [Key, Column(Order = 0)]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        public int StatisticId { get; set; }

        public StatisticLevel StatisticLevel { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Statistic Statistic { get; set; }
    }
}