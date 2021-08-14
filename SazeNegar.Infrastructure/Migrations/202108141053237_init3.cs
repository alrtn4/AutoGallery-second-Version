namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "AddedDate", c => c.String());
            AddColumn("dbo.Carts", "AddedDate", c => c.String());
            DropColumn("dbo.Cars", "PersianAddedDate");
            DropColumn("dbo.Carts", "PersianAddedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "PersianAddedDate", c => c.String());
            AddColumn("dbo.Cars", "PersianAddedDate", c => c.String());
            DropColumn("dbo.Carts", "AddedDate");
            DropColumn("dbo.Cars", "AddedDate");
        }
    }
}
