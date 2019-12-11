using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class LoginUserModel
    {
        [Required(ErrorMessage = "A felhasználónevet kötelező megadni!")]
        [Display(Name = "Felhasználónév")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A jelszót kötelező megadni!")]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }
    }
}