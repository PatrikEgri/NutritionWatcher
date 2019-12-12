using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class UpdateStyleViewModel
    {
        [Required(ErrorMessage = "Válassz egy stílust!")]
        public int StyleId { get; set; }
        [Display(Name = "Stílus")]
        public List<StyleModel> Styles { get; set; }
    }
}