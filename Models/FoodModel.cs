using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class FoodModel
    {        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Protein { get; set; }
        [Required]
        public float Fat { get; set; }
        [Required]
        public float Hydrocarbonate { get; set; }
        [Required]
        public int Gramm { get; set; }

        public bool HasValue()
        {
            return (Name != "" && Name != null) && (Gramm != 0);
        }
    }
}