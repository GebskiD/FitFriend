using FitFriend.Models;
using FitFriend.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;



namespace FitFriend.Controllers
{
    public class FoodController : Controller
    {

        private ApplicationDbContext _context;


        public FoodController()
        {
            _context = new ApplicationDbContext();            
        }


        //public JsonResult Autocomplete(string term)
        //{
        //    var model = _context.Products
        //        .Where(m => m.Name.StartsWith(term))
        //        .Select(m => new { label = m.Name });

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products.Any(m => m.Name == model.Name))
                {
                    TempData["message"] = "Product already exist!";
                    return View();
                }
                else
                {
                    var product = new Product
                    {
                        Name = model.Name,
                        Carbs = model.Carbs,
                        Fats = model.Fats,
                        Proteins = model.Proteins,
                        Weight = model.Weight,
                        Calories = (model.Fats * 9) + (model.Carbs * 4) + (model.Proteins * 4)

                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    TempData["message"] = "Product " + model.Name + " created" ;
                    return View();
                }
            }
            return View(model);
        }

        

        public ActionResult MealRemove(int Id,string date)
        {

            var Removal = _context.MealComponents
               .Find(Id);
            
            _context.MealComponents.Remove(Removal);
            _context.SaveChanges();
            return RedirectToAction("FoodTable","Food", new {data = date });
        }


        [ChildActionOnly]
        public ActionResult TablePartial(FoodTableViewModel model, int mealNumber)
        {
            Product prd = new Product();
            FoodTableViewModel PartialModel = new FoodTableViewModel();

            PartialModel.Components = model.Components
                .Where(m => m.MealNumber == mealNumber)
                .ToList();

            foreach (var item in PartialModel.Components)
            {
                prd.Calories += item.Product.Calories;
                prd.Carbs += item.Product.Carbs;
                prd.Proteins += item.Product.Proteins;
                prd.Fats += item.Product.Fats;
            }

            model.MealTotal.Add(prd);
            model.AddComponents(prd);

            return PartialView("_TablePartial", PartialModel);
        }




        public ActionResult FoodTable(string data)
        {
            FoodTableViewModel mdl = new FoodTableViewModel();
            var QueryDate = FoodTableViewModel.GetDate(data);

            mdl.TableDate = QueryDate.Date.ToShortDateString();

            var UserId = User.Identity.GetUserId();
                mdl.Components = _context.MealComponents
                    .Where(m => m.Date.Day == QueryDate.Day && m.UserId == UserId)
                    .ToList();

                mdl.GetProducts();

                mdl.SelectedMealNumbers = mdl.Components.Select(m => m.MealNumber)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToList();

                return View(mdl);

        }
        

        //public ActionResult AddToFoodTable()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult AddToFoodTable(ProductsListViewModel model)
        {
            if (model.IsChecked != null)
            {
                for (int i = 0; i < model.IsChecked.Count(); i++)
                {
                    if (model.IsChecked[i])
                    {
                        var Components = new MealComponents
                        {
                            Quantity = model.Quantities[i],
                            ProductId = model.ProductIds[i],
                            Date = model.date,
                            MealNumber = model.MealNumber[i],
                            UserId = User.Identity.GetUserId()
                        };
                            _context.MealComponents.Add(Components);
                        _context.SaveChanges();
                    }
                }
                var dataFormated = model.date.Date.ToString("yyyy-MM-dd");
                return RedirectToAction("FoodTable","Food",new {data = dataFormated  });
            }

            return View();
        }




        //public ActionResult AddMeal()
        //{
        //    return View();
        //}


        [HttpGet]
        public ActionResult AddMeal(string SearchString , string data)
        {
            
            ProductsListViewModel mealModel = new ProductsListViewModel();
            mealModel.date = FoodTableViewModel.GetDate(data);

            if (String.IsNullOrEmpty(SearchString))
            {
                TempData["message"] = "Below are 5 last used products";

                var lastFiveProducts = _context.MealComponents
                    .OrderByDescending(p => p.Date)
                    .Select(p => p.Product)
                    .Take(5)
                    .ToList();
                mealModel.SelectedProducts = lastFiveProducts;
                return View(mealModel);
            }
            else
            {
               var model = _context.Products
                    .Where(m => m.Name.StartsWith(SearchString))
                    .ToList();

                mealModel.SelectedProducts = model;
                return View(mealModel);
            }
        }
    }
}