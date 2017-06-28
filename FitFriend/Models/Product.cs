using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace FitFriend.Models
{
    public class Product
    {
        [Column("ProductId")]
        public int Id { get; set; }

        
        [Display(Name = "Name")]
        public string Name { get; set; }

        
        [Display(Name = "Carbs")]
        public int Carbs { get; set; }

        
        [Display(Name = "Fats")]
        public int Fats { get; set; }

        
        [Display(Name = "Proteins")]
        public int Proteins { get; set; }

        
        [Display(Name = "Weight")]
        public int Weight { get; set; }


        [Display(Name ="Calories")]
        public int Calories { get; set; }


    }
}