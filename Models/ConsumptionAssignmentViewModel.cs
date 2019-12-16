using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class ConsumptionAssignmentViewModel
    {
        [Required(ErrorMessage = "Az étel kiválasztása kötelező!")]
        public int FoodId { get; set; }
        [Display(Name = "Étel")]
        
        public List<FoodModel> Foods { get; set; }
        [Required(ErrorMessage = "A fogyasztás kiválasztása kötelező!")]
        public int ConsumptionId { get; set; }
        [Display(Name = "Fogyasztás")]
        
        public List<ConsumptionModel> Consumptions { get; set; }
        [Display(Name = "Fogyasztott mennyiség (g)")]
        [Required(ErrorMessage = "A mennyiség megadása kötelező!")]
        public int Gramm { get; set; }
    }
}