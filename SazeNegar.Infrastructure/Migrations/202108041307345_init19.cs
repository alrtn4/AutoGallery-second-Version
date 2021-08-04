namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init19 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CarModelCarClasses", newName: "CarClassCarModels");
            RenameTable(name: "dbo.CarClassCarModels", newName: "CarClassCarModels1");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CarClassCarModels1", newName: "CarClassCarModels");
            RenameTable(name: "dbo.CarClassCarModels", newName: "CarModelCarClasses");
        }
    }
}
