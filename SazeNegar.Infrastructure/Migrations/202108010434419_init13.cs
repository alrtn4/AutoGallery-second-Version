namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "Subject");
        }
    }
}
