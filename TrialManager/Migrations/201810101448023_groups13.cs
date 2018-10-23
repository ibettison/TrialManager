namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups13 : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("dbo.ContactTrialGroupModels", "ContactId", "dbo.ContactsModels");
            DropForeignKey("dbo.ContactTrialGroupModels", "TrialGroupId", "dbo.TrialGroupModels");
            DropForeignKey("dbo.TrialGroupTrialModels", "TrialGroupId", "dbo.TrialGroupModels");
            DropForeignKey("dbo.TrialGroupTrialModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropIndex("dbo.ContactTrialGroupModels", new[] { "TrialGroupId" });
            DropIndex("dbo.ContactTrialGroupModels", new[] { "ContactId" });
            DropIndex("dbo.TrialGroupTrialModels", new[] { "TrialId" });
            DropIndex("dbo.TrialGroupTrialModels", new[] { "TrialGroupId" });
            DropTable("dbo.ContactTrialGroupModels");
            DropTable("dbo.TrialGroupTrialModels");
        }
        
        public override void Up()
        {
            CreateTable(
                "dbo.TrialGroupTrialModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialId = c.Int(nullable: false),
                        TrialGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactTrialGroupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialGroupId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TrialGroupTrialModels", "TrialGroupId");
            CreateIndex("dbo.TrialGroupTrialModels", "TrialId");
            CreateIndex("dbo.ContactTrialGroupModels", "ContactId");
            CreateIndex("dbo.ContactTrialGroupModels", "TrialGroupId");
            AddForeignKey("dbo.TrialGroupTrialModels", "TrialId", "dbo.TrialFeasibilityModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrialGroupTrialModels", "TrialGroupId", "dbo.TrialGroupModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactTrialGroupModels", "TrialGroupId", "dbo.TrialGroupModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContactTrialGroupModels", "ContactId", "dbo.ContactsModels", "Id", cascadeDelete: true);
        }
    }
}
