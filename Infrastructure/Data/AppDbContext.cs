using Application.Interfaces.JobCard;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<VehicleOwner> VehicleOwners { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<TechnicianNote> TechnicianNotes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<InvoiceService> InvoiceServices { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<InvoicePart> InvoiceParts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JobCard> JobCards { get; set; }
        public DbSet<Domain.Entities.JobCardService> JobCardServices { get; set; }
        public DbSet<Domain.Entities.JobCardPart> JobCardParts { get; set; }
        public DbSet<Domain.Entities.ManualPart> ManualParts { get; set; }
        public DbSet<Domain.Entities.ManualService> ManualServices { get; set; }
        public DbSet<Domain.Entities.ManualInvoicePart> ManualInvoiceParts { get; set; }
        public DbSet<Domain.Entities.ManualInvoiceService> ManualInvoiceServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleOwner)
                .WithMany(o => o.Vehicles)
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Vehicle)
                .WithMany(v => v.Invoices)
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Recommendation>()
                .HasOne(r => r.Vehicle)
                .WithMany(v => v.Recommendations)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TechnicianNote>()
                .HasOne(t => t.Vehicle)
                .WithMany(v => v.TechnicianNotes)
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TechnicianNote>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.TechnicianNotes)
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceService>()
                .HasOne(n => n.Invoice)
                .WithMany(i => i.InvoiceServices)
                .HasForeignKey(n => n.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoiceService>()
                .HasOne(e => e.Service)
                .WithMany(s => s.InvoiceServices)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoicePart>()
                .HasOne(ip => ip.Invoice)
                .WithMany(i => i.InvoiceParts)
                .HasForeignKey(ip => ip.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InvoicePart>()
                .HasOne(ip => ip.Part)
                .WithMany(p => p.InvoiceParts)
                .HasForeignKey(ip => ip.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobCard>()
                .HasOne(j => j.Vehicle)
                .WithMany(v => v.JobCards)
                .HasForeignKey(j => j.VehicleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobCard>()
                .HasOne(j => j.Mechanic)
                .WithMany(u => u.JobCards)
                .HasForeignKey(j => j.MechanicID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobCard>()
                .HasOne(j => j.Vehicle)
                .WithMany(v => v.JobCards)
                .HasForeignKey(j => j.VehicleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobCard>()
                .HasOne(j => j.Mechanic)
                .WithMany(u => u.JobCards)
                .HasForeignKey(j => j.MechanicID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Domain.Entities.JobCardService>()
                .HasOne(js => js.JobCard)
                .WithMany(j => j.JobCardServices)
                .HasForeignKey(js => js.JobCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.JobCardService>()
                .HasOne(js => js.Service)
                .WithMany(s => s.JobCardServices)
                .HasForeignKey(js => js.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobCardPart>()
                .HasOne(jp => jp.JobCard)
                .WithMany(j => j.JobCardParts)
                .HasForeignKey(jp => jp.JobCardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobCardPart>()
                .HasOne(jp => jp.Part)
                .WithMany(p => p.JobCardParts)
                .HasForeignKey(jp => jp.PartId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
