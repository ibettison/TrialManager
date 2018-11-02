namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDatesTobeBlank1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrialActiveModels", "TargetFPFV", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "ActualFPFV", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "EstimatedLPLV", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "ActualLPLV", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "EOSNotificationDue", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "EOSNotificationSubmitted", c => c.DateTime());
            AlterColumn("dbo.TrialActiveModels", "SiteCloseDown", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "ClinicalStudyReportDue", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "ClinicalStudyReportSubmitted", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "EudraCTUploadDue", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "EudraCTUpload", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "Archiving", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "ArchivingPeriod", c => c.DateTime());
            AlterColumn("dbo.TrialCloseDownModels", "Destruction", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrialCloseDownModels", "Destruction", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "ArchivingPeriod", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "Archiving", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "EudraCTUpload", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "EudraCTUploadDue", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "ClinicalStudyReportSubmitted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialCloseDownModels", "ClinicalStudyReportDue", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "SiteCloseDown", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "EOSNotificationSubmitted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "EOSNotificationDue", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "ActualLPLV", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "EstimatedLPLV", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "ActualFPFV", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TrialActiveModels", "TargetFPFV", c => c.DateTime(nullable: false));
        }
    }
}
