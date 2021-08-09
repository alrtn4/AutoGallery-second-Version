﻿using SazeNegar.Core.Models;
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
        public int? categoryId { get; set; }
        public string searchString { get; set; }
        public long? priceFrom { get; set; }
        public long? priceTo { get; set; }
        public string brands { get; set; }
        public int pageNumber { get; set; }
        public int take { get; set; }
        public string sort { get; set; }

        public string BrandIds { get; set; }
        public string GroupIds { get; set; }
        public string ProductIds { get; set; }
    }
}