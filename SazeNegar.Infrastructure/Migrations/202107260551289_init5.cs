﻿namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Special", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Special");
        }
    }
}
