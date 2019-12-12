using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UpdateEmailViewModel
    {
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Az E-Mail cím megadása kötelező!")]
        public string Email { get; set; }
    }
}