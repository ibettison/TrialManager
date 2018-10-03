namespace TrialManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessTypesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactName = c.String(),
                        Organisation = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        ContactStatusId = c.Int(nullable: false),
                        ContactNotes = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactStatusModels", t => t.ContactStatusId, cascadeDelete: true)
                .Index(t => t.ContactStatusId);
            
            CreateTable(
                "dbo.ContactStatusModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactStatusName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractTypesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiseaseTherapyAreaModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiseaseTherapyAreaName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentTypesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrantTypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrantTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Who = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        TrialId = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.TrialFeasibilityModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        TrialTitle = c.String(),
                        ApplicationReference = c.String(),
                        BhNumber = c.String(),
                        TrialTypeId = c.Int(nullable: false),
                        Commercial = c.String(),
                        PhaseId = c.Int(nullable: false),
                        SampleSize = c.String(),
                        GrantTypeId = c.Int(nullable: false),
                        FundingStream = c.String(),
                        GrantDeadlineDate = c.DateTime(nullable: false),
                        UniConsultancyAgreement = c.Boolean(nullable: false),
                        NhsConsultancyAgreement = c.Boolean(nullable: false),
                        DiseaseTherapyAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiseaseTherapyAreaModels", t => t.DiseaseTherapyAreaId, cascadeDelete: true)
                .ForeignKey("dbo.GrantTypeModels", t => t.GrantTypeId, cascadeDelete: true)
                .ForeignKey("dbo.PhaseModels", t => t.PhaseId, cascadeDelete: true)
                .ForeignKey("dbo.TrialTypeModels", t => t.TrialTypeId, cascadeDelete: true)
                .Index(t => t.TrialTypeId)
                .Index(t => t.PhaseId)
                .Index(t => t.GrantTypeId)
                .Index(t => t.DiseaseTherapyAreaId);
            
            CreateTable(
                "dbo.PhaseModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhaseName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrialTypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrialTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResearchPassportModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ContactId = c.Int(nullable: false),
                        RenewalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactsModels", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RolesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffPassportModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        ContractTypeId = c.Int(nullable: false),
                        ContractEndDate = c.DateTime(nullable: false),
                        TypeofAccessId = c.Int(nullable: false),
                        AccessExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccessTypesModels", t => t.TypeofAccessId, cascadeDelete: true)
                .ForeignKey("dbo.ContactsModels", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.ContractTypesModels", t => t.ContractTypeId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.ContractTypeId)
                .Index(t => t.TypeofAccessId);
            
            CreateTable(
                "dbo.TrialContactsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        TrialId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactsModels", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.RolesModels", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.ContactId)
                .Index(t => t.TrialId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TrialDocumentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        UploadedBy = c.String(),
                        DocumentLink = c.String(),
                        DocumentFileName = c.String(),
                        DocumentVersion = c.String(),
                        DocumentType = c.Int(nullable: false),
                        DocumentDescription = c.String(),
                        TrialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .ForeignKey("dbo.DocumentTypesModels", t => t.DocumentType, cascadeDelete: true)
                .Index(t => t.DocumentType)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.TrialLocationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrialRecordsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        WhoChanged = c.String(),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        FieldName = c.String(),
                        ReasonForChange = c.String(),
                        TrialId = c.Int(nullable: false),
                        Original = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.TrialRemindersModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reminder = c.String(),
                        TrialId = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.TrialSetupModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectIdentifier = c.String(),
                        ResearchDevelopmentId = c.String(),
                        GrantFunderRef = c.String(),
                        SponsorRef = c.String(),
                        RecRef = c.String(),
                        EudractRef = c.String(),
                        IrasId = c.String(),
                        RecruitmentTarget = c.String(),
                        TrialLocationId = c.Int(nullable: false),
                        RecDate = c.DateTime(nullable: false),
                        HraDate = c.DateTime(nullable: false),
                        CtaDate = c.DateTime(nullable: false),
                        TrialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialLocationModels", t => t.TrialLocationId, cascadeDelete: true)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialLocationId)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.TrialsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        TrialTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrialStartedModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Started = c.String(),
                        TrialId = c.Int(nullable: false),
                        Reason = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrialFeasibilityModels", t => t.TrialId, cascadeDelete: true)
                .Index(t => t.TrialId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserRole = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrialStartedModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialSetupModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialSetupModels", "TrialLocationId", "dbo.TrialLocationModels");
            DropForeignKey("dbo.TrialRemindersModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialRecordsModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialDocumentsModels", "DocumentType", "dbo.DocumentTypesModels");
            DropForeignKey("dbo.TrialDocumentsModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialContactsModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialContactsModels", "RoleId", "dbo.RolesModels");
            DropForeignKey("dbo.TrialContactsModels", "ContactId", "dbo.ContactsModels");
            DropForeignKey("dbo.StaffPassportModels", "ContractTypeId", "dbo.ContractTypesModels");
            DropForeignKey("dbo.StaffPassportModels", "ContactId", "dbo.ContactsModels");
            DropForeignKey("dbo.StaffPassportModels", "TypeofAccessId", "dbo.AccessTypesModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ResearchPassportModels", "ContactId", "dbo.ContactsModels");
            DropForeignKey("dbo.NotesModels", "TrialId", "dbo.TrialFeasibilityModels");
            DropForeignKey("dbo.TrialFeasibilityModels", "TrialTypeId", "dbo.TrialTypeModels");
            DropForeignKey("dbo.TrialFeasibilityModels", "PhaseId", "dbo.PhaseModels");
            DropForeignKey("dbo.TrialFeasibilityModels", "GrantTypeId", "dbo.GrantTypeModels");
            DropForeignKey("dbo.TrialFeasibilityModels", "DiseaseTherapyAreaId", "dbo.DiseaseTherapyAreaModels");
            DropForeignKey("dbo.ContactsModels", "ContactStatusId", "dbo.ContactStatusModels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TrialStartedModels", new[] { "TrialId" });
            DropIndex("dbo.TrialSetupModels", new[] { "TrialId" });
            DropIndex("dbo.TrialSetupModels", new[] { "TrialLocationId" });
            DropIndex("dbo.TrialRemindersModels", new[] { "TrialId" });
            DropIndex("dbo.TrialRecordsModels", new[] { "TrialId" });
            DropIndex("dbo.TrialDocumentsModels", new[] { "TrialId" });
            DropIndex("dbo.TrialDocumentsModels", new[] { "DocumentType" });
            DropIndex("dbo.TrialContactsModels", new[] { "RoleId" });
            DropIndex("dbo.TrialContactsModels", new[] { "TrialId" });
            DropIndex("dbo.TrialContactsModels", new[] { "ContactId" });
            DropIndex("dbo.StaffPassportModels", new[] { "TypeofAccessId" });
            DropIndex("dbo.StaffPassportModels", new[] { "ContractTypeId" });
            DropIndex("dbo.StaffPassportModels", new[] { "ContactId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ResearchPassportModels", new[] { "ContactId" });
            DropIndex("dbo.TrialFeasibilityModels", new[] { "DiseaseTherapyAreaId" });
            DropIndex("dbo.TrialFeasibilityModels", new[] { "GrantTypeId" });
            DropIndex("dbo.TrialFeasibilityModels", new[] { "PhaseId" });
            DropIndex("dbo.TrialFeasibilityModels", new[] { "TrialTypeId" });
            DropIndex("dbo.NotesModels", new[] { "TrialId" });
            DropIndex("dbo.ContactsModels", new[] { "ContactStatusId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TrialStartedModels");
            DropTable("dbo.TrialsModels");
            DropTable("dbo.TrialSetupModels");
            DropTable("dbo.TrialRemindersModels");
            DropTable("dbo.TrialRecordsModels");
            DropTable("dbo.TrialLocationModels");
            DropTable("dbo.TrialDocumentsModels");
            DropTable("dbo.TrialContactsModels");
            DropTable("dbo.StaffPassportModels");
            DropTable("dbo.RolesModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ResearchPassportModels");
            DropTable("dbo.TrialTypeModels");
            DropTable("dbo.PhaseModels");
            DropTable("dbo.TrialFeasibilityModels");
            DropTable("dbo.NotesModels");
            DropTable("dbo.GrantTypeModels");
            DropTable("dbo.DocumentTypesModels");
            DropTable("dbo.DiseaseTherapyAreaModels");
            DropTable("dbo.ContractTypesModels");
            DropTable("dbo.ContactStatusModels");
            DropTable("dbo.ContactsModels");
            DropTable("dbo.AccessTypesModels");
        }
    }
}
