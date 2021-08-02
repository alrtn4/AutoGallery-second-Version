namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "PersianDateTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "PersianDateTime");
        }
    }
}
