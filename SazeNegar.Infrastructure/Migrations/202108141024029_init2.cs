namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "PersianAddedDate", c => c.String());
            AddColumn("dbo.Carts", "PersianAddedDate", c => c.String());
            DropColumn("dbo.Cars", "PersianDateTime");
            DropColumn("dbo.Carts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Date", c => c.Int(nullable: false));
            AddColumn("dbo.Cars", "PersianDateTime", c => c.String());
            DropColumn("dbo.Carts", "PersianAddedDate");
            DropColumn("dbo.Cars", "PersianAddedDate");
        }
    }
}
