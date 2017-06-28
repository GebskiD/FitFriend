using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitFriend.Models
{
    public class MealComponents
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public float Quantity { get; set; }

        [Range(1, 8)]
        public int MealNumber { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}