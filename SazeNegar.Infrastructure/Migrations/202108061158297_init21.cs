namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarsInfoes", "ImageTop1", c => c.String());
            DropColumn("dbo.CarsInfoes", "carImage1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarsInfoes", "carImage1", c => c.String());
            DropColumn("dbo.CarsInfoes", "ImageTop1");
        }
    }
}
