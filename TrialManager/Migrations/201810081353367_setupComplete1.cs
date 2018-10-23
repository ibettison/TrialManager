namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setupComplete1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrialSetupCompleteModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Completed = c.String(),
                        TrialId = c.Int(nullable: false),
                        Reason = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrialSetupCompleteModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropIndex("dbo.TrialSetupCompleteModels", new[] { "TrialId" });
            DropTable("dbo.TrialSetupCompleteModels");
        }
    }
}
