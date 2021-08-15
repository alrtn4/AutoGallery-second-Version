using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class SingleCarViewModel
    {
        public SingleCarViewModel(Cars cars)
        {
            this.PersianAddedDate = cars.InsertDate != null ? new PersianDateTime(cars.InsertDate.Value).ToString() : "-";
            this.Cars = cars;
        }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }
        public Cars Cars { get; set; }
        public List<Gallery> Galleries { get; set; }
        public  List<CarDateViewModel> CarDateList { get; set; }
    }
    public class CarDateViewModel
    {
        public CarDateViewModel(Cars cars)
        {
            this.PersianAddedDate = cars.InsertDate != null ? new PersianDateTime(cars.InsertDate.Value).ToString() : "-";
            this.Cars = cars;
        }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }
        public Cars Cars { get; set; }
    }
}