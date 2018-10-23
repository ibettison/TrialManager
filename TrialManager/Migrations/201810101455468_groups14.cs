namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactTrialGroupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialGroupId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactsModels", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.TrialGroupModels", t => t.TrialGroupId, cascadeDelete: true)
                .Index(t => t.TrialGroupId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.TrialGroupTrialModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialId = c.Int(nullable: false),
                        TrialGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialGroupModels", t => t.TrialGroupId, cascadeDelete: true)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId)
                .Index(t => t.TrialGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrialGroupTrialModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialGroupTrialModels", "TrialGroupId", "dbo.TrialGroupModels");
            DropForeignKey("dbo.ContactTrialGroupModels", "TrialGroupId", "dbo.TrialGroupModels");
            DropForeignKey("dbo.ContactTrialGroupModels", "ContactId", "dbo.ContactsModels");
            DropIndex("dbo.TrialGroupTrialModels", new[] { "TrialGroupId" });
            DropIndex("dbo.TrialGroupTrialModels", new[] { "TrialId" });
            DropIndex("dbo.ContactTrialGroupModels", new[] { "ContactId" });
            DropIndex("dbo.ContactTrialGroupModels", new[] { "TrialGroupId" });
            DropTable("dbo.TrialGroupTrialModels");
            DropTable("dbo.ContactTrialGroupModels");
        }
    }
}
