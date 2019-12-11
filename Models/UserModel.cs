using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Display(Name = "Felhasználónév")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Display(Name = "Keresztnév")]
        //[Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public string Firstname { get; set; }
        [Display(Name = "Vezetéknév")]
        //[Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public string Lastname { get; set; }
        [Display(Name = "Jelszó")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public string Password { get; set; }
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Display(Name = "Stílus")]
        //[Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public StyleModel Style { get; set; }
        [Display(Name = "Jogosultság")]
        //[Required(ErrorMessage = "Ezt a mezőt kötelező kitölteni!", AllowEmptyStrings = false)]
        public PermissionModel Permission { get; set; }
    }
}