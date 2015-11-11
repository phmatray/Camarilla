using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum StatisticCategory
    {
        [Display(Name = "Physique")] Physical,
        [Display(Name = "Mental")] Mental
    }
}