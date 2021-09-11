using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;
using SazeNegar.Core.Models;

namespace SazeNegar.Infrastructure
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleHeadLine> ArticleHeadLines { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<StaticContentType> StaticContentTypes { get; set; }
        public DbSet<StaticContentDetail> StaticContentDetails { get; set; }
        public DbSet<OurTeam> OurTeams { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<GalleryVideo> GalleryVideos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<ProjectGallery> ProjectGalleries { get; set; }
        public DbSet<OurService> OurServices { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarClassCarModels> CarClassCarModels { get; set; }
        public DbSet<CarClass> CarClasses { get; set; }
        public DbSet<CarsInfo> CarsInfos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Core.Controllers.CarsController> enums { get; set; }
    }
}