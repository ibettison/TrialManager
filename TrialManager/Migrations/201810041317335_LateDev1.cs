namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LateDev1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrialLateDevelopmentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialId = c.Int(nullable: false),
                        ClinicalTrialsGovRef = c.String(),
                        LocalCC = c.DateTime(nullable: false),
                        SponsorGreenLight = c.DateTime(nullable: false),
                        LocalSiteInitiation = c.DateTime(nullable: false),
                        TargetFpfvDate = c.DateTime(nullable: false),
                        MultiSiteInitiation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrialLateDevelopmentModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropIndex("dbo.TrialLateDevelopmentModels", new[] { "TrialId" });
            DropTable("dbo.TrialLateDevelopmentModels");
        }
    }
}
