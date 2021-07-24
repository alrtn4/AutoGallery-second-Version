namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarModelCarClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarModelId = c.Int(nullable: false),
                        CarClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarClasses", t => t.CarClassId, cascadeDelete: true)
                .ForeignKey("dbo.CarModels", t => t.CarModelId, cascadeDelete: true)
                .Index(t => t.CarModelId)
                .Index(t => t.CarClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarModelCarClasses", "CarModelId", "dbo.CarModels");
            DropForeignKey("dbo.CarModelCarClasses", "CarClassId", "dbo.CarClasses");
            DropIndex("dbo.CarModelCarClasses", new[] { "CarClassId" });
            DropIndex("dbo.CarModelCarClasses", new[] { "CarModelId" });
            DropTable("dbo.CarModelCarClasses");
        }
    }
}
