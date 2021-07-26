namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "Special", c => c.String());
            AddColumn("dbo.Carts", "Tag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "Tag");
            DropColumn("dbo.Carts", "Special");
        }
    }
}
