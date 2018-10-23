namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactTrialGroupModels", "ReadOnly", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactTrialGroupModels", "ReadOnly");
        }
    }
}
