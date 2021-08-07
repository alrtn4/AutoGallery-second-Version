namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 400),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        ArticleCategoryId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleCategories", t => t.ArticleCategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ArticleCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 400),
                        Message = c.String(nullable: false, maxLength: 800),
                        AddedDate = c.DateTime(),
                        ParentId = c.Int(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.ArticleComments", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ArticleHeadLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 700),
                        Description = c.String(),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 300),
                        ArticleId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Avatar = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 300),
                        LastName = c.String(nullable: false, maxLength: 300),
                        Information = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.Role_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        RoleNameLocal = c.String(maxLength: 300),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 300),
                        Name = c.String(maxLength: 300),
                        DisplayPriority = c.Int(nullable: false),
                        ControllerName = c.String(maxLength: 300),
                        ActionName = c.String(maxLength: 300),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 100),
                        CarModelId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarModels", t => t.CarModelId, cascadeDelete: true)
                .Index(t => t.CarModelId);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 100),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Price = c.String(),
                        Gear = c.String(),
                        Sunroof = c.String(),
                        Navigation = c.String(),
                        Image = c.String(),
                        Special = c.String(),
                        PersianDateTime = c.String(),
                        BrandsId = c.Int(nullable: false),
                        CarsInfoId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandsId, cascadeDelete: true)
                .ForeignKey("dbo.CarsInfoes", t => t.CarsInfoId, cascadeDelete: true)
                .Index(t => t.BrandsId)
                .Index(t => t.CarsInfoId);
            
            CreateTable(
                "dbo.CarsInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Doors = c.String(),
                        Break = c.String(),
                        Explanation = c.String(),
                        DetailsSunroof = c.String(),
                        DetailsHeight = c.String(),
                        DetailsMirror = c.String(),
                        DetailsSencor = c.String(),
                        DetailsElectricity = c.String(),
                        DetailsMap = c.String(),
                        DetailsKeyless = c.String(),
                        DetailsGuide = c.String(),
                        FeaturesComputer = c.String(),
                        FeaturesRightKey = c.String(),
                        FeaturesLeftKey = c.String(),
                        FeaturesTempreture = c.String(),
                        FeaturesCruse = c.String(),
                        FeaturesJack = c.String(),
                        FeaturesCurtain = c.String(),
                        FeaturesHeater = c.String(),
                        TechnicalEngineShort = c.String(),
                        TechnicalEngineDetails = c.String(),
                        TechnicalBreakShort = c.String(),
                        TechnicalBreakDetails = c.String(),
                        TechnicalVentilationShort = c.String(),
                        TechnicalVentilationDetails = c.String(),
                        ImageTop1 = c.String(),
                        ImageTop2 = c.String(),
                        ImageTop3 = c.String(),
                        ImageTop4 = c.String(),
                        ImageTop5 = c.String(),
                        ImageTop6 = c.String(),
                        ImageNav1 = c.String(),
                        ImageNav2 = c.String(),
                        ImageNav3 = c.String(),
                        ImageNav4 = c.String(),
                        ImageNav5 = c.String(),
                        ImageNav6 = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        Date = c.Int(nullable: false),
                        Link = c.String(),
                        Special = c.String(),
                        Tag = c.String(),
                        CarsId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarsId)
                .Index(t => t.CarsId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        Type = c.String(),
                        CarsId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarsId, cascadeDelete: true)
                .Index(t => t.CarsId);
            
            CreateTable(
                "dbo.CarClassCarModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarModelId = c.Int(nullable: false),
                        CarClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarClasses", t => t.CarClassId, cascadeDelete: true)
                .ForeignKey("dbo.CarModels", t => t.CarModelId, cascadeDelete: true)
                .Index(t => t.CarModelId)
                .Index(t => t.CarClassId);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 600),
                        Email = c.String(nullable: false, maxLength: 600),
                        Subject = c.String(),
                        Message = c.String(nullable: false),
                        IsViewed = c.Boolean(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false),
                        Answer = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Video = c.String(),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 200),
                        TableName = c.String(maxLength: 100),
                        EntityId = c.Int(nullable: false),
                        Action = c.String(maxLength: 100),
                        ActionDate = c.DateTime(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OurServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OurTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Image = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        ProjectCategoryId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectCategories", t => t.ProjectCategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        ProjectId = c.Int(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 400),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        Description = c.String(),
                        Image = c.String(),
                        AddedDate = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                        ServiceCategoryId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategories", t => t.ServiceCategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ServiceCategoryId);
            
            CreateTable(
                "dbo.StaticContentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Link = c.String(),
                        StaticContentTypeId = c.Int(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaticContentTypes", t => t.StaticContentTypeId, cascadeDelete: true)
                .Index(t => t.StaticContentTypeId);
            
            CreateTable(
                "dbo.StaticContentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 600),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Image = c.String(),
                        Description = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarClassCarModels1",
                c => new
                    {
                        CarClass_Id = c.Int(nullable: false),
                        CarModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CarClass_Id, t.CarModel_Id })
                .ForeignKey("dbo.CarClasses", t => t.CarClass_Id, cascadeDelete: true)
                .ForeignKey("dbo.CarModels", t => t.CarModel_Id, cascadeDelete: true)
                .Index(t => t.CarClass_Id)
                .Index(t => t.CarModel_Id);
            
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
