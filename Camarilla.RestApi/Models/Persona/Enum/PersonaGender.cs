using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum PersonaGender
    {
        [Display(Name = "Masculin")] Male,
        [Display(Name = "Féminin")] Female
    }
}