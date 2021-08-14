using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SazeNegar.Core.Models;

namespace SazeNegar.Web.ViewModels
{
    public class CarBrandsInfoViewModel 
    {
        public CarBrandsInfoViewModel()
        {

        }
        public List<Cars> CarsList { get; set; }
        public List<Brands> BrandsList { get; set; }
        public List<CarsInfo> CarsInfoList { get; set; }
        public Cars Cars { get; set; }
        public Brands Brands { get; set; }
        public CarsInfo CarsInfo { get; set; }

        public string PersianAddeDate { get; set; }
    }
}
