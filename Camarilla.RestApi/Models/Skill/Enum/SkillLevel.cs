using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum SkillLevel
    {
        [Display(Name = "Aucun")] No = 0,
        [Display(Name = "Novice")] Novice = 1,
        [Display(Name = "Entrainé")] Trained = 2,
        [Display(Name = "Compétent")] Competent = 3,
        [Display(Name = "Expert")] Expert = 4,
        [Display(Name = "Maître")] Master = 5
    }
}