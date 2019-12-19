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
        [StringLength(50)]
        public string Username { get; set; }
        [Display(Name = "Keresztnév")]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Display(Name = "Vezetéknév")]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Display(Name = "Jelszó")]
        [StringLength(50)]
        public string Password { get; set; }
        [Display(Name = "E-Mail")]
        [StringLength(50)]
        public string Email { get; set; }
        [Display(Name = "Stílus")]
        public StyleModel Style { get; set; }
        [Display(Name = "Jogosultság")]
        public PermissionModel Permission { get; set; }
    }
}