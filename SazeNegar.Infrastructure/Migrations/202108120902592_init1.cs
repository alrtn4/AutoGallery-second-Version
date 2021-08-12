namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {

            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaticContentDetails", "StaticContentTypeId", "dbo.StaticContentTypes");
            DropForeignKey("dbo.Services", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Services", "ServiceCategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Projects", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectCategoryId", "dbo.ProjectCategories");
            DropForeignKey("dbo.CarClassCarModels", "CarModelId", "dbo.CarModels");
            DropForeignKey("dbo.CarClassCarModels", "CarClassId", "dbo.CarClasses");
            DropForeignKey("dbo.Galleries", "CarsId", "dbo.Cars");
            DropForeignKey("dbo.Carts", "CarsId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "CarsInfoId", "dbo.CarsInfoes");
            DropForeignKey("dbo.Cars", "BrandsId", "dbo.Brands");
            DropForeignKey("dbo.CarClassCarModels1", "CarModel_Id", "dbo.CarModels");
            DropForeignKey("dbo.CarClassCarModels1", "CarClass_Id", "dbo.CarClasses");
            DropForeignKey("dbo.Brands", "CarModelId", "dbo.CarModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RolePermissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "ParentId", "dbo.Permissions");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ArticleTags", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleHeadLines", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleComments", "ParentId", "dbo.ArticleComments");
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "ArticleCategoryId", "dbo.ArticleCategories");
            DropIndex("dbo.CarClassCarModels1", new[] { "CarModel_Id" });
            DropIndex("dbo.CarClassCarModels1", new[] { "CarClass_Id" });
            DropIndex("dbo.StaticContentDetails", new[] { "StaticContentTypeId" });
            DropIndex("dbo.Services", new[] { "ServiceCategoryId" });
            DropIndex("dbo.Services", new[] { "UserId" });
            DropIndex("dbo.ProjectGalleries", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Projects", new[] { "ProjectCategoryId" });
            DropIndex("dbo.CarClassCarModels", new[] { "CarClassId" });
            DropIndex("dbo.CarClassCarModels", new[] { "CarModelId" });
            DropIndex("dbo.Galleries", new[] { "CarsId" });
            DropIndex("dbo.Carts", new[] { "CarsId" });
            DropIndex("dbo.Cars", new[] { "CarsInfoId" });
            DropIndex("dbo.Cars", new[] { "BrandsId" });
            DropIndex("dbo.Brands", new[] { "CarModelId" });
            DropIndex("dbo.Permissions", new[] { "ParentId" });
            DropIndex("dbo.RolePermissions", new[] { "PermissionId" });
            DropIndex("dbo.RolePermissions", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Role_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ArticleTags", new[] { "ArticleId" });
            DropIndex("dbo.ArticleHeadLines", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropIndex("dbo.ArticleComments", new[] { "ParentId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "ArticleCategoryId" });
            DropTable("dbo.CarClassCarModels1");
            DropTable("dbo.Testimonials");
            DropTable("dbo.StaticContentTypes");
            DropTable("dbo.StaticContentDetails");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.ProjectGalleries");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectCategories");
            DropTable("dbo.Partners");
            DropTable("dbo.OurTeams");
            DropTable("dbo.OurServices");
            DropTable("dbo.Logs");
            DropTable("dbo.GalleryVideos");
            DropTable("dbo.Faqs");
            DropTable("dbo.ContactForms");
            DropTable("dbo.Certificates");
            DropTable("dbo.CarClassCarModels");
            DropTable("dbo.Galleries");
            DropTable("dbo.Carts");
            DropTable("dbo.CarsInfoes");
            DropTable("dbo.Cars");
            DropTable("dbo.CarClasses");
            DropTable("dbo.CarModels");
            DropTable("dbo.Brands");
            DropTable("dbo.Permissions");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ArticleTags");
            DropTable("dbo.ArticleHeadLines");
            DropTable("dbo.ArticleComments");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleCategories");
        }
    }
}
