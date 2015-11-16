using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum ClanCategory
    {
        [Display(Name = "Humain")] Human,
        [Display(Name = "Vampire")] Vampire,
        [Display(Name = "Goule")] Ghoul
    }
}