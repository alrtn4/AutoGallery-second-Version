using SazeNegar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazeNegar.Web.ViewModels
{
    public class ProjectInfoViewModel
    {
        public ProjectInfoViewModel()
        {

        }
        public ProjectInfoViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Description = project.Description;
            this.ShortDescription = project.ShortDescription;
            this.ProjectCategory = project.ProjectCategory != null ? project.ProjectCategory.Name : "-";
            this.PersianAddedDate = project.AddedDate != null ? new PersianDateTime(project.AddedDate.Value).ToString() : "-";
        }
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        //[Display(Name = "نویسنده")]
        //public string Author { get; set; }
        [Display(Name = "دسته بندی")]
        public string ProjectCategory { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public string PersianAddedDate { get; set; }

        [Display(Name = "توضیح کوتاه")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
    }

    public class TopProjectsViewModel
    {
        public TopProjectsViewModel()
        {
        }

        public TopProjectsViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Image = project.Image;
            this.PersianDate = project.AddedDate != null ? new PersianDateTime(project.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }

    public class ProjectListViewModel
    {
        public ProjectListViewModel()
        {
        }

        public ProjectListViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.ShortDescription = project.ShortDescription;
            this.Author = project.User != null ? $"{project.User.FirstName} {project.User.LastName}" : "-";
            this.Image = project.Image;
            this.AuthorAvatar = project.User.Avatar ?? "user-avatar.png";
            this.PersianDate = project.AddedDate != null ? new PersianDateTime(project.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
        public string Author { get; set; }
        public string AuthorAvatar { get; set; }
        public string Role { get; set; }
    }

    public class ProjectCategoriesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProjectCount { get; set; }
    }

    public class ProjectDetailsViewModel
    {
        public ProjectDetailsViewModel()
        {

        }
        public ProjectDetailsViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Image = project.Image;
            this.ShortDescription = project.ShortDescription;
            this.Description = project.Description;
            this.Author = project.User != null ? $"{project.User.FirstName} {project.User.LastName}" : "-";
            this.PersianDate = project.AddedDate != null ? new PersianDateTime(project.AddedDate.Value).ToString("dddd d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PersianDate { get; set; }
    }

    public class LatestProjectsViewModel
    {
        public LatestProjectsViewModel()
        {
        }

        public LatestProjectsViewModel(Project project)
        {
            this.Id = project.Id;
            this.Title = project.Title;
            this.Image = project.Image;
            this.PersianDate = project.AddedDate != null ? new PersianDateTime(project.AddedDate.Value).ToString("d MMMM yyyy") : "-";
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string PersianDate { get; set; }
    }
}