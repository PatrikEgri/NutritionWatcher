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
        [Required(ErrorMessage = "Kötelező megadni a nevet!")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kötelező megadni a fehérjetartalmat!")]
        public float Protein { get; set; }
        [Required(ErrorMessage = "Kötelező megadni a zsírtartalmat!")]
        public float Fat { get; set; }
        [Required(ErrorMessage = "Kötelező megadni a szénhidráttartalmat!")]
        public float Hydrocarbonate { get; set; }
        [Required(ErrorMessage = "Kötelező megadni a tömeget!")]
        public int Gramm { get; set; }
    }
}