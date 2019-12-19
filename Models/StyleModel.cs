﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NutritionWatcher.Models
{
    public class StyleModel
    {
        public int Id { get; set; }
        [Display(Name = "Stílus")]
        [StringLength(10)]
        public string Name { get; set; }
    }
}