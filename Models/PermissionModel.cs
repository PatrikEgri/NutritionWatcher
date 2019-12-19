using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace NutritionWatcher.Models
{
    public class PermissionModel
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
    }
}