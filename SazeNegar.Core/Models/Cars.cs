using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace SazeNegar.Core.Models
{

    public class Cars : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} باید از 100 کارکتر کمتر باشد")]
        public string Title { get; set; }
        [Display(Name = "قیمت")]
        public string Price { get; set; }
        [Display(Name = "دنده")]
        public string Gear { get; set; }
        [Display(Name = "سانروف")]
        public string Sunroof { get; set; }
        [Display(Name = "راهبری")]
        public string Navigation { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [Display(Name = "ویژه")]
        public string Special { get; set; }
        public string PersianDateTime { get; set; }

        public int BrandsId { get; set; }
        public virtual Brands Brand { get; set; }
        public int CarsInfoId { get; set; }
        public virtual CarsInfo CarsInfo { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual  ICollection<Gallery> Gallery { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }

}

