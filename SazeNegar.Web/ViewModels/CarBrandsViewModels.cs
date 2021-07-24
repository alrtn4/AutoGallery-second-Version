using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SazeNegar.Core.Models;

namespace SazeNegar.Web.ViewModels
{
    public class CarBrandsViewModel 
    {
        public List<Brands> CarBrandsList { get; set; }
        public List<Cars> CarsList { get; set; }
        public List<Brands> BrandsList { get; set; }
        public Cars Cars { get; set; }
        public Brands Brands { get; set; }
    }
}
