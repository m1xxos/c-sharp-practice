using IncommingPost.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace IncommingPost.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; }
    public DbSet<Sender> Senders { get; set; }
    public DbSet<Executor> Executors { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<RegistrationLog> RegistrationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Document entity
        modelBuilder.Entity<Document>()
            .HasOne(d => d.ReceivedFrom)
            .WithMany(s => s.Documents)
            .HasForeignKey(d => d.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>()
            .HasOne(d => d.ClassifiedAs)
            .WithMany(t => t.Documents)
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure many-to-many relationship between Document and Executor
        modelBuilder.Entity<Document>()
            .HasMany(d => d.AssignedTo)
            .WithMany(e => e.AssignedDocuments)
            .UsingEntity(j => j.ToTable("DocumentExecutor"));

        // Configure one-to-many relationship between Document and RegistrationLog
        modelBuilder.Entity<RegistrationLog>()
            .HasOne(r => r.Document)
            .WithMany(d => d.RecordedIn)
            .HasForeignKey(r => r.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}