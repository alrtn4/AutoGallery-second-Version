namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init14 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "temp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "temp", c => c.Int(nullable: false));
        }
    }
}
