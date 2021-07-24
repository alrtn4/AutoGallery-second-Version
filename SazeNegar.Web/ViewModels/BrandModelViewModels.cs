using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SazeNegar.Core.Models;

namespace SazeNegar.Web.ViewModels
{
    public class BrandModelViewModel 
    {
        public List<Cars> CarsList { get; set; }
        public List<Brands> BrandsList { get; set; }
        public List<CarModel> ModelsList { get; set; }
        public Cars Cars { get; set; }
        public Brands Brands { get; set; }
        public CarModel CarModel { get; set; }
    }
}
