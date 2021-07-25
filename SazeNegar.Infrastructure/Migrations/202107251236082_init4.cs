namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CarsId", "dbo.Cars");
            DropIndex("dbo.Carts", new[] { "CarsId" });
            AlterColumn("dbo.Carts", "CarsId", c => c.Int());
            CreateIndex("dbo.Carts", "CarsId");
            AddForeignKey("dbo.Carts", "CarsId", "dbo.Cars", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "CarsId", "dbo.Cars");
            DropIndex("dbo.Carts", new[] { "CarsId" });
            AlterColumn("dbo.Carts", "CarsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "CarsId");
            AddForeignKey("dbo.Carts", "CarsId", "dbo.Cars", "Id", cascadeDelete: true);
        }
    }
}
