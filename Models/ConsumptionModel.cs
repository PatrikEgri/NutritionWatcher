using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class ConsumptionModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A dátum megadása kötelező")]
        [Display(Name = "Dátum")]
        [StringLength(12)]
        public string Date { get; set; }
        [Required(ErrorMessage = "Az idő megadása kötelező")]
        [Display(Name = "Idő")]
        [StringLength(10)]
        public string Time { get; set; }
        public UserModel User { get; set; }

        public bool IsThisWeek()
        {
            return DateTime.Parse(Date) <= DateTime.Today && DateTime.Parse(Date) >= DateTime.Today.AddDays(-6);
        }

        public bool IsThisMonth()
        {
            return DateTime.Parse(Date) <= DateTime.Today && DateTime.Parse(Date) >= DateTime.Today.AddDays(-30);
        }
    }
}