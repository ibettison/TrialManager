namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contacUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactsModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactsModels", "UserId");
        }
    }
}
