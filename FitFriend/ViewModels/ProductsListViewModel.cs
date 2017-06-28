using FitFriend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace FitFriend.ViewModels
{
    public class ProductsListViewModel
    {
        public ProductsListViewModel()
        {
            SelectedProducts = new List<Product>();
        }

        public List<float> Quantities { get; set; }
        public List<int> ProductIds { get; set; }
        public List<bool> IsChecked { get; set; }
        public DateTime date { get; set; }

        public int[] MealNumber { get; set; }

        public List<Product> SelectedProducts { get; set; }

        public MealComponents Meal { get; set; }


    }
   }
