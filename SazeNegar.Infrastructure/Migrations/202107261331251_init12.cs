namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "BrandId" });
            AlterColumn("dbo.Cars", "BrandId", c => c.Int());
            CreateIndex("dbo.Cars", "BrandId");
            AddForeignKey("dbo.Cars", "BrandId", "dbo.Brands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.Cars", new[] { "BrandId" });
            AlterColumn("dbo.Cars", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "BrandId");
            AddForeignKey("dbo.Cars", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
        }
    }
}
