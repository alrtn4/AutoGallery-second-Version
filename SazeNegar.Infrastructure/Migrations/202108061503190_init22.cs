namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "CarsInfo_Id", "dbo.CarsInfoes");
            DropIndex("dbo.Cars", new[] { "CarsInfo_Id" });
            RenameColumn(table: "dbo.Cars", name: "CarsInfo_Id", newName: "CarsInfoId");
            AlterColumn("dbo.Cars", "CarsInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "CarsInfoId");
            AddForeignKey("dbo.Cars", "CarsInfoId", "dbo.CarsInfoes", "Id", cascadeDelete: true);
            DropColumn("dbo.Cars", "CarInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "CarInfoId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cars", "CarsInfoId", "dbo.CarsInfoes");
            DropIndex("dbo.Cars", new[] { "CarsInfoId" });
            AlterColumn("dbo.Cars", "CarsInfoId", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "CarsInfoId", newName: "CarsInfo_Id");
            CreateIndex("dbo.Cars", "CarsInfo_Id");
            AddForeignKey("dbo.Cars", "CarsInfo_Id", "dbo.CarsInfoes", "Id");
        }
    }
}
