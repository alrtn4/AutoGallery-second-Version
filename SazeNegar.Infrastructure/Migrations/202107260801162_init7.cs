namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CarsId", "dbo.Cars");
            DropIndex("dbo.Carts", new[] { "CarsId" });
            DropColumn("dbo.Carts", "CarsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "CarsId", c => c.Int());
            CreateIndex("dbo.Carts", "CarsId");
            AddForeignKey("dbo.Carts", "CarsId", "dbo.Cars", "Id");
        }
    }
}
