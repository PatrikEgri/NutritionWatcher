using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionWatcher.Models
{
    public class CalorieViewModel
    {
        public int Id { get; set; }
        public ConsumptionModel Consumption { get; set; }
        public FoodModel Food { get; set; }
        public int ConsumedGramms { get; set; }

        public float GetConsumedCalorie()
        {
            return (Food.Protein * 4 + Food.Hydrocarbonate * 4 + Food.Fat * 9) / Food.Gramm * ConsumedGramms;
        }

        public float GetConsumedProtein()
        {
            return Food.Protein / Food.Gramm * ConsumedGramms;
        }

        public float GetConsumedFat()
        {
            return Food.Fat / Food.Gramm * ConsumedGramms;
        }

        public float GetConsumedHydrocarbonate()
        {
            return Food.Hydrocarbonate / Food.Gramm * ConsumedGramms;
        }

        public bool IsThisWeek()
        {
            return DateTime.Parse(Consumption.Date) <= DateTime.Today && DateTime.Parse(Consumption.Date) >= DateTime.Today.AddDays(-6);
        }

        public bool IsThisMonth()
        {
            return DateTime.Parse(Consumption.Date) <= DateTime.Today && DateTime.Parse(Consumption.Date) >= DateTime.Today.AddDays(-30);
        }
        
    }
}