using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace INYTWebsite.Models
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
        public virtual DbSet<LookupTable> LookupTable { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<TradeAdditionalQuestions> TradeAdditionalQuestions { get; set; }
        public virtual DbSet<TradeMaster> TradeMaster { get; set; }
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

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventType)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RecordId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerRegistration>(entity =>
            {
                entity.Property(e => e.CustomerEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerMobileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPassword)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.HasAgreedTc).HasColumnName("HasAgreedTC");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.JobCloseReason)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.JobReference)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.JobRequiredAddress).IsUnicode(false);

                entity.Property(e => e.JobRequiredDate).HasColumnType("datetime");

                entity.Property(e => e.JobTradesperson)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LookupTable>(entity =>
            {
                entity.Property(e => e.LookupType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LookupValue)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsArchived).HasColumnName("isArchived");

                entity.Property(e => e.MessageAttachment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MessageBody)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.MessageDate).HasColumnType("datetime");

                entity.Property(e => e.MessageSubject)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.RatingComments)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ratings1).HasColumnName("Ratings");
            });

            modelBuilder.Entity<TradeAdditionalQuestions>(entity =>
            {
                entity.Property(e => e.AdditionalQuestion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AnswerOptionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AnswerOptions).HasMaxLength(4000);
            });

            modelBuilder.Entity<TradeMaster>(entity =>
            {
                entity.Property(e => e.Trade)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tradesperson>(entity =>
            {
                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanySize).HasMaxLength(10);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeactivationReason)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}
