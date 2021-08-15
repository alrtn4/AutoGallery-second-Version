using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class CarouselViewModel
    {
        public CarouselViewModel(Cart cart)
        {
            this.PersianAddedDate = cart.InsertDate != null ? new PersianDateTime(cart.InsertDate.Value).ToString() : "-";
            this.Cart = cart;
        }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }
        public Cart Cart { get; set; }
    }
}