namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required_Fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrialFeasibilityModels", "ShortName", c => c.String(nullable: false));
            AlterColumn("dbo.TrialFeasibilityModels", "TrialTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrialFeasibilityModels", "TrialTitle", c => c.String());
            AlterColumn("dbo.TrialFeasibilityModels", "ShortName", c => c.String());
        }
    }
}
