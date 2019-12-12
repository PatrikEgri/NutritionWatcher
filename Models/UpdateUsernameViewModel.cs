using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UpdateUsernameViewModel
    {
        [Display(Name = "Felhasználónév")]
        [Required(ErrorMessage = "Kötelező kitölteni ezt a mezőt!")]
        public string Username { get; set; }
    }
}