namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDatesTobeBlank : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrialFeasibilityModels", "GrantDeadlineDate", c => c.DateTime());
            AlterColumn("dbo.ResearchPassportModels", "StartDate", c => c.DateTime());
            AlterColumn("dbo.ResearchPassportModels", "EndDate", c => c.DateTime());
            AlterColumn("dbo.ResearchPassportModels", "RenewalDate", c => c.DateTime());
            AlterColumn("dbo.TrialSetupModels", "ApprovalDate", c => c.DateTime());
            AlterColumn("dbo.TrialSetupModels", "RecDate", c => c.DateTime());
            AlterColumn("dbo.TrialSetupModels", "HraDate", c => c.DateTime());
            AlterColumn("dbo.TrialSetupModels", "CtaDate", c => c.DateTime());
            AlterColumn("dbo.TrialSetupModels", "FunderMileStoneReport", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrialSetupModels", "FunderMileStoneReport", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialSetupModels", "CtaDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialSetupModels", "HraDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialSetupModels", "RecDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialSetupModels", "ApprovalDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ResearchPassportModels", "RenewalDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ResearchPassportModels", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ResearchPassportModels", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialFeasibilityModels", "GrantDeadlineDate", c => c.DateTime(nullable: false));
        }
    }
}
