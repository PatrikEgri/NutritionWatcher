using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class RegistrationUserModel
    {
        [Display(Name = "Felhasználónév")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        public string Username { get; set; }

        [Display(Name = "Keresztnév")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        public string Firstname { get; set; }

        [Display(Name = "Vezetéknév")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        public string Lastname { get; set; }

        [Display(Name = "Jelszó")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "A két jelszó nem egyezik!")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!")]
        public string Email { get; set; }
    }
}