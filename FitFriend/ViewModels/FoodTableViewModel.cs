using FitFriend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitFriend.ViewModels
{
    public class FoodTableViewModel
    {
        public FoodTableViewModel()
        {
            MealTotal = new List<Product>();
            TotalSum = new Product();
        }


        public string TableDate { get; set; }
        public List<int> SelectedMealNumbers { get; set; }
        public List<MealComponents> Components { get; set; }
        public List<Product> MealTotal { get; set; }
        public Product TotalSum { get; set; }

        public void AddComponents(Product prod)
        {
            TotalSum.Carbs += prod.Carbs;
            TotalSum.Fats += prod.Fats;
            TotalSum.Proteins += prod.Proteins;
            TotalSum.Calories += prod.Calories;
        }



        public void GetProducts()
        {
            foreach (var item in Components)
            {
                Product prd = new Product()
                {
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    Weight = Convert.ToInt16(item.Product.Weight * item.Quantity),
                    Fats = Convert.ToInt16(item.Product.Fats * item.Quantity),
                    Carbs = Convert.ToInt16(item.Product.Carbs * item.Quantity),
                    Proteins = Convert.ToInt16(item.Product.Proteins * item.Quantity),
                    Calories = Convert.ToInt16(item.Product.Calories * item.Quantity)
                };
                item.Product = prd;
            }
        }


        public static DateTime GetDate(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                DateTime Date = DateTime.Now.Date;
                return Date;
            }
            else
            {
                try
                {
                    DateTime Date = DateTime.ParseExact(data, "dd-MM-yyyy", null);
                    return Date;

                }
                catch (Exception)
                {
                    DateTime Date = DateTime.ParseExact(data, "yyyy-MM-dd", null);
                    Date = Date.Add(TimeSpan.Parse("00:00:00"));
                    return Date;

                }
            }
        }


    }
}