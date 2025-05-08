using Microsoft.EntityFrameworkCore;
using OutcommingPost.Domain;

namespace OutcommingPost.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Initiator> Initiators { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Document entity
            modelBuilder.Entity<Document>()
                .Property(d => d.Subject)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Document>()
                .Property(d => d.Content)
                .IsRequired();

            modelBuilder.Entity<Document>()
                .Property(d => d.RegistrationNumber)
                .HasMaxLength(50);

            // Configure Initiator entity
            modelBuilder.Entity<Initiator>()
                .Property(i => i.FullName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Initiator>()
                .Property(i => i.Position)
                .HasMaxLength(100);

            // Configure Receiver entity
            modelBuilder.Entity<Receiver>()
                .Property(r => r.FullName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}