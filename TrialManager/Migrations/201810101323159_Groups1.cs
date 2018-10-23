namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrialGroupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrialGroupModels");
        }
    }
}
