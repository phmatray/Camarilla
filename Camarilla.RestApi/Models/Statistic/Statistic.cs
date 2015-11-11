using System.Collections.Generic;

namespace Camarilla.RestApi.Models
{
    public class Statistic : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public StatisticCategory StatisticCategory { get; set; }
        public virtual ICollection<PersonaStatistic> PersonaStatistics { get; set; } = new List<PersonaStatistic>();
    }
}