using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum ClanCategory
    {
        [Display(Name = "Aucun")] None,
        [Display(Name = "Humain")] Human,
        [Display(Name = "Vampire")] Vampire,
        [Display(Name = "Goule")] Ghoul
    }
}