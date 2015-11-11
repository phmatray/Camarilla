using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum SkillCategory
    {
        [Display(Name = "Normal")] Normal,
        [Display(Name = "Bonus")] Bonus,
        [Display(Name = "Special")] Special
    }
}