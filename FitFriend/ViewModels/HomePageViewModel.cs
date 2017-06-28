using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitFriend.ViewModels
{
    public class HomePageViewModel
    {
        public List<string> ValueX { get; set; }
        public List<float> ValueY { get; set; }

        public void CompleteChart(List<DateTime> AxisX_Dates, List<float> AxisY, int chartRange)
        {
            var AcctDate = DateTime.Now;
            var data = AcctDate.Date;

            if (!AxisX_Dates.Any())
            {
                AxisX_Dates.Add(data);
                AxisY.Add(0);
            }

            if (AxisX_Dates.Last() != data )
            {
                AxisX_Dates.Add(data);
                AxisY.Add(0);
            }

            for (int i = 0; i < chartRange; i++)
            {

                if (AxisX_Dates[i].Date != data.AddDays(-chartRange + i))
                {
                    AxisX_Dates.Insert(i, data.AddDays(-chartRange + i));
                    AxisY.Insert(i, 0);
                }
            }
        }
    }
}