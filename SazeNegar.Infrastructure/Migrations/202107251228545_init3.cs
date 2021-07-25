namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "CarsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "CarsId");
            AddForeignKey("dbo.Carts", "CarsId", "dbo.Cars", "Id", cascadeDelete: true);
            DropColumn("dbo.Carts", "Special");
            DropColumn("dbo.Carts", "Tag");
            DropColumn("dbo.Carts", "Image");
            DropColumn("dbo.Carts", "Price");
            DropColumn("dbo.Carts", "Brand");
            DropColumn("dbo.Carts", "Gear");
            DropColumn("dbo.Carts", "Sunroof");
            DropColumn("dbo.Carts", "Navigation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Navigation", c => c.String());
            AddColumn("dbo.Carts", "Sunroof", c => c.String());
            AddColumn("dbo.Carts", "Gear", c => c.String());
            AddColumn("dbo.Carts", "Brand", c => c.String());
            AddColumn("dbo.Carts", "Price", c => c.String());
            AddColumn("dbo.Carts", "Image", c => c.String());
            AddColumn("dbo.Carts", "Tag", c => c.String());
            AddColumn("dbo.Carts", "Special", c => c.String());
            DropForeignKey("dbo.Carts", "CarsId", "dbo.Cars");
            DropIndex("dbo.Carts", new[] { "CarsId" });
            DropColumn("dbo.Carts", "CarsId");
        }
    }
}
