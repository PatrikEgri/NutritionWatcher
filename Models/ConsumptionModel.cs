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
        public string Date { get; set; }
        public string Time { get; set; }
        public int UserId { get; set; }

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