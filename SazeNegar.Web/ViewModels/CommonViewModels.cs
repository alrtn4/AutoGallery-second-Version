using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SazeNegar.Core.Models;

namespace SazeNegar.Web.ViewModels
{
    public class FooterViewModel
    {
        public StaticContentDetail Map { get; set; }
        public StaticContentDetail Phone { get; set; }
        public StaticContentDetail Email { get; set; }
        public StaticContentDetail Address { get; set; }
        public StaticContentDetail Youtube { get; set; }
        public StaticContentDetail Instagram { get; set; }
        public StaticContentDetail Twitter { get; set; }
        public StaticContentDetail Facebook { get; set; }
        public StaticContentDetail Pinterest { get; set; }
    }
    public class ContactUsViewModel
    {
        public StaticContentDetail ContactInfo { get; set; }
        public StaticContentDetail Phone { get; set; }
        public StaticContentDetail Email { get; set; }
        public StaticContentDetail Address { get; set; }
        public StaticContentDetail Youtube { get; set; }
        public StaticContentDetail Instagram { get; set; }
        public StaticContentDetail Twitter { get; set; }
        public StaticContentDetail Facebook { get; set; }
        public StaticContentDetail Pinterest { get; set; }
    }

    public class GalleryPageViewModel
    {
        public List<Gallery> Images { get; set; }
        public List<GalleryVideo> Videos { get; set; }
    }

    public class ProjectsViewModel
    {
        public string Description { get; set; }

        public List<ProjectsQuotesViewModel> Quotes { get; set; }
    }
    public class ProjectsQuotesViewModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
    }

    public class AboutUsViewModel
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
    }
}