namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "AddedDate", c => c.DateTime());
            AlterColumn("dbo.Carts", "AddedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "AddedDate", c => c.String());
            AlterColumn("dbo.Cars", "AddedDate", c => c.String());
        }
    }
}
