using FitFriend.Models;
using FitFriend.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitFriend.Controllers
{

    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();

        }


        public ActionResult GoogleChart()
        {
            return View();
        }

        public ActionResult Index()
        {
            HomePageViewModel mdl = new HomePageViewModel();

            var userId = User.Identity.GetUserId();
            int chartRange = 3;

            var acctualDate = DateTime.Now.Date;
            var startDate = acctualDate.AddDays(-chartRange);

              var Chart = _context.MealComponents
                .Where(m => m.Date >= startDate && m.UserId == userId)
                .ToList();

            var AxisX_Dates = Chart
                .Select(m => m.Date.Date)
                .Distinct()
                .OrderBy(m => m.Date.Date)
                .ToList();


            var AxisY = Chart
                    .OrderBy(m => m.Date.Date)
                    .GroupBy(m => m.Date.Date)
                    .Select(m => m.Select(n => n.Quantity * n.Product.Calories).Sum())
                    .ToList();


            mdl.CompleteChart(AxisX_Dates, AxisY, chartRange);

            mdl.ValueX = AxisX_Dates.Select(m => m.Date.ToString("dd/MM/yyyy")).ToList();
            mdl.ValueY = AxisY;

            return View(mdl);
        }
    }
}