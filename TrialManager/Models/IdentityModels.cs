using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Trialmanager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<AccessTypesModels> AccessTypesModels { get; set; }
        public DbSet<NotesModels> NotesModels { get; set; }
        public DbSet<ContactStatusModels> ContactStatusModels { get; set; }
        public DbSet<RolesModels> RolesModels { get; set; }
        public DbSet<ContactsModels> ContactsModels { get; set; }
        public DbSet<PhaseModels> PhaseModels { get; set; }
        public DbSet<ContractTypesModels> ContractTypesModels { get; set; }
        public DbSet<DiseaseTherapyAreaModels> DiseaseTherapyAreaModels { get; set; }
        public DbSet<GrantTypeModels> GrantTypeModels { get; set; }
        public DbSet<ResearchPassportModels> ResearchPassportModels { get; set; }
        public DbSet<StaffPassportModels> StaffPassportModels { get; set; }
        public DbSet<TrialFeasibilityModels> TrialFeasibilityModels { get; set; }
        public DbSet<TrialRecordsModels> TrialRecordsModels { get; set; }
        public DbSet<TrialsModels> TrialsModels { get; set; }
        public DbSet<TrialTypeModels> TrialTypeModels { get; set; }
        public DbSet<TrialSetupModels> TrialSetupModels { get; set; }
        public DbSet<TrialLocationModels> TrialLocationModels { get; set; }
        public DbSet<TrialRemindersModels> TrialRemindersModels { get; set; }
        public DbSet<TrialDocumentsModels> TrialDocumentsModels { get; set; }
        public DbSet<DocumentTypesModels> DocumentTypesModels { get; set; }
        public DbSet<TrialStartedModels> TrialStartedModels { get; set; }
        public System.Data.Entity.DbSet<Trialmanager.Models.TrialContactsModels> TrialContactsModels { get; set; }
    }
}