namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reminderChecked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrialRemindersModels", "Checked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrialRemindersModels", "Checked");
        }
    }
}
