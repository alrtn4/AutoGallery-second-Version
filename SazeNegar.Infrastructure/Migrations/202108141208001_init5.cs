namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cars", "AddedDate");
            DropColumn("dbo.Carts", "AddedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "AddedDate", c => c.DateTime());
            AddColumn("dbo.Cars", "AddedDate", c => c.DateTime());
        }
    }
}
