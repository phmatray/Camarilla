using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum ClanKind
    {
        [Display(Name = "Humain")] Human,
        [Display(Name = "Brujah")] Brujah,
        [Display(Name = "Gangrel Urbain")] UrbanGangrel,
        [Display(Name = "Gangrel Rural")] RuralGangrel,
        [Display(Name = "Malkavien")] Malkavien,
        [Display(Name = "Nosferatu")] Nosferatu,
        [Display(Name = "Toréador")] Toreador,
        [Display(Name = "Tremère")] Tremere,
        [Display(Name = "Ventrue")] Venture
    }
}