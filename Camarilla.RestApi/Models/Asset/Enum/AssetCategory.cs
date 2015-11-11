// Dealing with Enum in MVC 5.1
// http://www.codeproject.com/Articles/776908/Dealing-with-Enum-in-MVC?fid=1860573&df=90&mpp=50&prof=False&sort=Position&view=Normal&spc=Compact

using System.ComponentModel.DataAnnotations;

namespace Camarilla.RestApi.Models
{
    public enum AssetCategory
    {
        [Display(Name = "Physique")] Physical,
        [Display(Name = "Mental")] Mental,
        [Display(Name = "Social")] Social,
        [Display(Name = "Surnaturel")] Surnatural
    }
}