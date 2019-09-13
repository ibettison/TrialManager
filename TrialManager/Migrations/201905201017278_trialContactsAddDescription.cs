namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trialContactsAddDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrialContactsModels", "RoleDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrialContactsModels", "RoleDescription");
        }
    }
}
