using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class ServiceInfoViewModel
    {
        public ServiceInfoViewModel()
        {

        }
        public ServiceInfoViewModel(Service service)
        {
            this.Id = service.Id;
            this.Title = service.Title;
            this.Description = service.Description;
            this.ServiceCategory = service.ServiceCategory != null ? service.ServiceCategory.Title : "-";
            this.PersianAddedDate = service.AddedDate != null ? new PersianDateTime(service.AddedDate.Value).ToString() : "-";
        }
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "دسته بندی")]
        public string ServiceCategory { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }

        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class TopServicesViewModel
    {
        public TopServicesViewModel()
        {
        }

        public TopServicesViewModel(Service service)
        {
            this.Id = service.Id;
            this.Title = service.Title;
            this.Image = service.Image;
            this.PersianDate = service.AddedDate != null ? new PersianDateTime(service.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }

    public class ServiceListViewModel
    {
        public ServiceListViewModel()
        {
        }

        public ServiceListViewModel(Service service)
        {
            this.Id = service.Id;
            this.Title = service.Title;
            this.Description = service.Description;
            this.Author = service.User != null ? $"{service.User.FirstName} {service.User.LastName}" : "-";
            this.Image = service.Image;
            this.AuthorAvatar = service.User.Avatar ?? "user-avatar.png";
            this.PersianDate = service.AddedDate != null ? new PersianDateTime(service.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
        public string Author { get; set; }
        public string AuthorAvatar { get; set; }
        public string Role { get; set; }
    }

    public class ServiceCategoriesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectCount { get; set; }
    }

    public class ServiceDetailsViewModel
    {
        public ServiceDetailsViewModel()
        {

        }
        public ServiceDetailsViewModel(Service service)
        {
            this.Id = service.Id;
            this.Title = service.Title;
            this.Image = service.Image;
            this.Description = service.Description;
            this.Description = service.Description;
            this.Author = service.User != null ? $"{service.User.FirstName} {service.User.LastName}" : "-";
            this.PersianDate = service.AddedDate != null ? new PersianDateTime(service.AddedDate.Value).ToString("dddd d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PersianDate { get; set; }
    }

    public class LatestservicesViewModel
    {
        public LatestservicesViewModel()
        {
        }

        public LatestservicesViewModel(Service service)
        {
            this.Id = service.Id;
            this.Title = service.Title;
            this.Image = service.Image;
            this.PersianDate = service.AddedDate != null ? new PersianDateTime(service.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }
}