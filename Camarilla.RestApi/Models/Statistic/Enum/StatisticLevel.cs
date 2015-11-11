using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum StatisticLevel
    {
        [Display(Name = "Novice")] Novice = 1,
        [Display(Name = "Entrain�")] Trained = 2,
        [Display(Name = "Comp�tent")] Competent = 3,
        [Display(Name = "Expert")] Expert = 4,
        [Display(Name = "Ma�tre")] Master = 5
    }
}