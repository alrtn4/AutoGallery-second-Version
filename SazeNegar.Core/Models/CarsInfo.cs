using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace SazeNegar.Core.Models
{

    public class CarsInfo : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} باید از 100 کارکتر کمتر باشد")]
        public string Title { get; set; }
        [Display(Name = "درها")]
        public string Doors { get; set; }
        [Display(Name = "ترمز")]
        public string Break { get; set; }
        [Display(Name = "توضیحات")]
        public string Explanation { get; set; }
        [Display(Name = "سانروف")]
        public string DetailsSunroof { get; set; }
        [Display(Name = "ارتفاع آینه")]
        public string DetailsHeight { get; set; }
        [Display(Name = "برقی آینه")]
        public string DetailsMirror { get; set; }
        [Display(Name = "حسگر")]
        public string DetailsSencor { get; set; }
        [Display(Name = "خروجی برق")]
        public string DetailsElectricity { get; set; }
        [Display(Name = "نقشه")]
        public string DetailsMap { get; set; }
        [Display(Name = "بدون کلید")]
        public string DetailsKeyless { get; set; }
        [Display(Name = "راهنما")]
        public string DetailsGuide { get; set; }
        [Display(Name = "کامپیوتر")]
        public string FeaturesComputer { get; set; }
        [Display(Name = "قفل شاگرد")]
        public string FeaturesRightKey { get; set; }
        [Display(Name = "قفل راننده")]
        public string FeaturesLeftKey { get; set; }
        [Display(Name = "حرارت")]
        public string FeaturesTempreture { get; set; }
        [Display(Name = "کروز")]
        public string FeaturesCruse { get; set; }
        [Display(Name = "جک")]
        public string FeaturesJack { get; set; }
        [Display(Name = "پرده")]
        public string FeaturesCurtain { get; set; }
        [Display(Name = "گرمکن")]
        public string FeaturesHeater { get; set; }
        [Display(Name = "موتور کوتاه")]
        public string TechnicalEngineShort { get; set; }
        [Display(Name = "موتور جزییات")]
        public string TechnicalEngineDetails { get; set; }
        [Display(Name = "ترمر کوتاه")]
        public string TechnicalBreakShort { get; set; }
        [Display(Name = "ترمز جزییات")]
        public string TechnicalBreakDetails { get; set; }
        [Display(Name = "تهویه کوتاه")]
        public string TechnicalVentilationShort { get; set; }
        [Display(Name = "تهویه جزییات")]
        public string TechnicalVentilationDetails { get; set; }
        [Display(Name = "تصویر 1")]
        public string Image1 { get; set; }
        [Display(Name = "تصویر 2")]
        public string Image2 { get; set; }
        [Display(Name = "تصویر 3")]
        public string Image3 { get; set; }
        [Display(Name = "تصویر 4")]
        public string Image4 { get; set; }
        [Display(Name = "تصویر 5")]
        public string Image5 { get; set; }
        [Display(Name = "تصویر 6")]
        public string Image6 { get; set; }

        public virtual ICollection<Cars> Cars { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }

}

