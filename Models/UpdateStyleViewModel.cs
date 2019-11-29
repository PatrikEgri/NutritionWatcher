using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionWatcher.Models
{
    public class UpdateStyleViewModel
    {
        public int UserId { get; set; }
        public int StyleId { get; set; }
        public List<StyleModel> Styles { get; set; }
    }
}