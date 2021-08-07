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
}