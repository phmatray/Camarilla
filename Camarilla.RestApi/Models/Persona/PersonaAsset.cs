using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Camarilla.RestApi.Models
{
    public class PersonaAsset : IModel
    {
        [Key, Column(Order = 0)]
        public int PersonaId { get; set; }

        [Key, Column(Order = 1)]
        public int AssetId { get; set; }

        public bool Checked { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual Asset Asset { get; set; }
    }
}