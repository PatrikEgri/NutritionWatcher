using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionWatcher.Models
{
    public class SummaryViewModel
    {
        public List<CalorieViewModel> CalorieViewModels { get; set; }
        public List<ConsumptionModel> ConsumptionModels { get; set; }
    }
}