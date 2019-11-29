using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class RegistrationUserModel
    {
        [Display(Name = "Felhasználónév")]
        [Required]
        public string Username { get; set; }
        [Display(Name = "Keresztnév")]
        [Required]
        public string Firstname { get; set; }
        [Display(Name = "Vezetéknév")]
        [Required]
        public string Lastname { get; set; }
        [Display(Name = "Jelszó")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        [Display(Name = "E-Mail")]
        [Required]
        public string Email { get; set; }

        public bool HasValue()
        {
            return Username != null && Username != "" && Password != null && Password != "";
        }
    }
}