using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SazeNegar.Core.Models;

namespace SazeNegar.Web.ViewModels
{
    public class CarModelViewModel 
    {
        public CarModel CarModel { get; set; }
        public List<CarClass> CarClassList { get; set; }
    }
}
