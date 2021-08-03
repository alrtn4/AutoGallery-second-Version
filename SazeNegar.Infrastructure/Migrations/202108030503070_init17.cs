namespace SpadCompanyPanel.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarsInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Doors = c.String(),
                        Break = c.String(),
                        Explanation = c.String(),
                        DetailsSunroof = c.String(),
                        DetailsHeight = c.String(),
                        DetailsMirror = c.String(),
                        DetailsSencor = c.String(),
                        DetailsElectricity = c.String(),
                        DetailsMap = c.String(),
                        DetailsKeyless = c.String(),
                        DetailsGuide = c.String(),
                        FeaturesComputer = c.String(),
                        FeaturesRightKey = c.String(),
                        FeaturesLeftKey = c.String(),
                        FeaturesTempreture = c.String(),
                        FeaturesCruse = c.String(),
                        FeaturesJack = c.String(),
                        FeaturesCurtain = c.String(),
                        FeaturesHeater = c.String(),
                        TechnicalEngineShort = c.String(),
                        TechnicalEngineDetails = c.String(),
                        TechnicalBreakShort = c.String(),
                        TechnicalBreakDetails = c.String(),
                        TechnicalVentilationShort = c.String(),
                        TechnicalVentilationDetails = c.String(),
                        Image1 = c.String(),
                        Image2 = c.String(),
                        Image3 = c.String(),
                        Image4 = c.String(),
                        Image5 = c.String(),
                        Image6 = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cars", "CarsInfo_Id", c => c.Int());
            CreateIndex("dbo.Cars", "CarsInfo_Id");
            AddForeignKey("dbo.Cars", "CarsInfo_Id", "dbo.CarsInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CarsInfo_Id", "dbo.CarsInfoes");
            DropIndex("dbo.Cars", new[] { "CarsInfo_Id" });
            DropColumn("dbo.Cars", "CarsInfo_Id");
            DropTable("dbo.CarsInfoes");
        }
    }
}
