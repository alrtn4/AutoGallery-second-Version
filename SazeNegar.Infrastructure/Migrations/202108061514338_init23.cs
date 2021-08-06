namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init23 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Cars", name: "BrandId", newName: "BrandsId");
            RenameIndex(table: "dbo.Cars", name: "IX_BrandId", newName: "IX_BrandsId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Cars", name: "IX_BrandsId", newName: "IX_BrandId");
            RenameColumn(table: "dbo.Cars", name: "BrandsId", newName: "BrandId");
        }
    }
}
