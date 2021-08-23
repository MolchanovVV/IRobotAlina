using IRobotAlina.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TenderDocumentsScraper.Data
{
    public class ApplicationDbContext : DbContext
    {        
        public DbSet<TenderPlatform> TenderPlatforms { get; set; }
        public DbSet<TenderMail> TenderMails { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderFileAttachment> TenderFileAttachments { get; set; }        
        public DbSet<ConfigurationItem> ConfigurationItems { get; set; }
        
        public ApplicationDbContext() : base()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=SRVP-BOTMKM;Database=tenders;User Id=sa;Password=482005Lol!",
                b => b.MigrationsAssembly("IRobotAlina.Web"));
        }           

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public void DetachAllEntities()
        {
            this.ChangeTracker.Entries().ToList()
                .ForEach(x => x.State = EntityState.Detached);
        }
    }
}
