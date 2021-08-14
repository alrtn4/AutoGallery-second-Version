using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class CarListViewModel
    {
        public Cars Cars { get; set; }
        public List<CarClass> CarClasses { get; set; }
    }

    public class CarGalleryViewModel
    {
        public Cars Cars { get; set; }
        public List<Gallery> Galleries { get; set; }
    }
    public class GridViewModel
    {
        public string searchString { get; set; }
        public string priceFrom { get; set; }
        public string priceTo { get; set; }
        public string brand { get; set; }
        public string options { get; set; }
        public int pageNumber { get; set; }
        public int take { get; set; }
        public string sort { get; set; }
    }
    public class CarAndDateViewModel
    {
        public CarAndDateViewModel(Cars cars)
        {
            this.PersianAddedDate = cars.InsertDate != null ? new PersianDateTime(cars.InsertDate.Value).ToString() : "-";
            this.Cars = cars;
        }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }
        public Cars Cars { get; set; }
        public string PersianAddedTime { get; set; }
    }
}