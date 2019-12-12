using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UpdatePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelenlegi jelszó")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Új jelszó")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "Új jelszó megerősítése")]
        public string ConfirmNewPassword { get; set; }
    }
}