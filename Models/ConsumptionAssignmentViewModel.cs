using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class ConsumptionAssignmentViewModel
    {
        public int FoodId { get; set; }
        [Display(Name = "Étel")]
        [Required]
        public List<FoodModel> Foods { get; set; }
        public int ConsumptionId { get; set; }
        [Display(Name = "Fogyasztás")]
        [Required]
        public List<ConsumptionModel> Consumptions { get; set; }
        [Display(Name = "Fogyasztott mennyiség (g)")]
        [Required]
        public int Gramm { get; set; }
    }
}