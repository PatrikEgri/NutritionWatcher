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
        public string Username { get; set; }
        [Display(Name = "Keresztnév")]
        public string Firstname { get; set; }
        [Display(Name = "Vezetéknév")]
        public string Lastname { get; set; }
        [Display(Name = "Jelszó")]
        public string Password { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Stílus")]
        public StyleModel Style { get; set; }
        [Display(Name = "Jogosultság")]
        public PermissionModel Permission { get; set; }

        public bool HasValue()
        {
            return Username != null && Username != "" && Password != null && Password != "";
        }
    }
}