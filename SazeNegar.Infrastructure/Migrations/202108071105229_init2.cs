namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CarsInfoes", "ImageTop1");
            DropColumn("dbo.CarsInfoes", "ImageTop2");
            DropColumn("dbo.CarsInfoes", "ImageTop3");
            DropColumn("dbo.CarsInfoes", "ImageTop4");
            DropColumn("dbo.CarsInfoes", "ImageTop5");
            DropColumn("dbo.CarsInfoes", "ImageTop6");
            DropColumn("dbo.CarsInfoes", "ImageNav1");
            DropColumn("dbo.CarsInfoes", "ImageNav2");
            DropColumn("dbo.CarsInfoes", "ImageNav3");
            DropColumn("dbo.CarsInfoes", "ImageNav4");
            DropColumn("dbo.CarsInfoes", "ImageNav5");
            DropColumn("dbo.CarsInfoes", "ImageNav6");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarsInfoes", "ImageNav6", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageNav5", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageNav4", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageNav3", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageNav2", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageNav1", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop6", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop5", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop4", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop3", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop2", c => c.String());
            AddColumn("dbo.CarsInfoes", "ImageTop1", c => c.String());
        }
    }
}
