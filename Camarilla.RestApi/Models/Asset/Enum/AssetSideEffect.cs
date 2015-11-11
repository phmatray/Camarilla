using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum AssetSideEffect
    {
        [Display(Name = "Bonus")] Bonus,
        [Display(Name = "Malus")] Malus
    }
}