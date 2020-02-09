using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INYTWebsite.Model
{
    public partial class INYTContext : DbContext
    {
        public INYTContext()
        {
        }

        public INYTContext(DbContextOptions<INYTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<CustomerRegistration> CustomerRegistration { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Tradesperson> Tradesperson { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=FUTURESOLUTIONS;Database=INYT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.AuditLogId).ValueGeneratedNever();

                entity.Property(e => e.EventType).IsUnicode(false);

                entity.Property(e => e.RecordId).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerRegistration>(entity =>
            {
                entity.Property(e => e.CustomerEmailAddress).IsUnicode(false);

                entity.Property(e => e.CustomerFirstName).IsUnicode(false);

                entity.Property(e => e.CustomerLastName).IsUnicode(false);

                entity.Property(e => e.CustomerMobileNumber).IsUnicode(false);

                entity.Property(e => e.CustomerPassword).IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Currency).IsUnicode(false);

                entity.Property(e => e.JobCloseReason).IsUnicode(false);

                entity.Property(e => e.JobReference).IsUnicode(false);

                entity.Property(e => e.JobRequiredAddress).IsUnicode(false);

                entity.Property(e => e.JobTradesperson).IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageAttachment).IsUnicode(false);

                entity.Property(e => e.MessageBody).IsUnicode(false);

                entity.Property(e => e.MessageSubject).IsUnicode(false);
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.Property(e => e.RatingComments).IsUnicode(false);
            });

            modelBuilder.Entity<Tradesperson>(entity =>
            {
                entity.Property(e => e.AddressLine1).IsUnicode(false);

                entity.Property(e => e.AddressLine2).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.CompanyNumber).IsUnicode(false);

                entity.Property(e => e.ContactNumber).IsUnicode(false);

                entity.Property(e => e.Country).IsUnicode(false);

                entity.Property(e => e.DeactivationReason).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Postcode).IsUnicode(false);

                entity.Property(e => e.Region).IsUnicode(false);

                entity.Property(e => e.Trade).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);
            });
        }
    }
}
