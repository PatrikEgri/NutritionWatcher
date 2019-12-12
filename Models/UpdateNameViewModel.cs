using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UpdateNameViewModel
    {
        [Display(Name = "Keresztnév")]
        [Required(ErrorMessage = "Kötelező megadni a keresztneved!")]
        public string Firstname { get; set; }

        [Display(Name = "Vezetéknév")]
        [Required(ErrorMessage = "Kötelező megadni a vezetékneved!")]
        public string Lastname { get; set; }
    }
}