using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace SazeNegar.Core.Models
{

    public class CarClass : IBaseEntity
    {
        public CarClass()
        {
            this.CarModels = new HashSet<CarModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

    }

}