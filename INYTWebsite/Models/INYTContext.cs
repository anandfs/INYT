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

        public virtual DbSet<AdditionalQuestionAnswers> AdditionalQuestionAnswers { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<AvailabilitySlots> AvailabilitySlots { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<CustomerRegistration> CustomerRegistration { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LookupTable> LookupTable { get; set; }
        public virtual DbSet<Membership> Membership { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<ServiceProvider> ServiceProvider { get; set; }
        public virtual DbSet<ServiceProviderAdditionalQuestions> ServiceProviderAdditionalQuestions { get; set; }
        public virtual DbSet<ServiceProviderAttachments> ServiceProviderAttachments { get; set; }
        public virtual DbSet<ServiceProviderBooking> ServiceProviderBooking { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<UnavailableDates> UnavailableDates { get; set; }

        public virtual DbSet<AvailabilityDates> AvailabilityDates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-GEMUACR\\MSSQLSERVER01;Database=INYT;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionalQuestionAnswers>(entity =>
            {
                entity.Property(e => e.AdditionalQuestion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Answer).HasMaxLength(4000);
            });

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

            modelBuilder.Entity<AvailabilitySlots>(entity =>
            {
                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.MinimumRate).HasColumnType("money");

                entity.Property(e => e.RateForAdditionalHour).HasColumnType("money");

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingAmount).HasColumnType("money");

                entity.Property(e => e.BookingDate).HasColumnType("datetime");

                entity.Property(e => e.BookingPaymentType)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BookingTime).HasColumnType("datetime");

                entity.Property(e => e.PaypalBookingReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.Property(e => e.KeyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KeyValue)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Provider)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerRegistration>(entity =>
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

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HasAgreedTc).HasColumnName("HasAgreedTC");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaidDate).HasColumnType("datetime");

                entity.Property(e => e.PaypalBookingReference)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
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

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.Property(e => e.BasicSubscriptionFee).HasColumnType("money");

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
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

            modelBuilder.Entity<ServiceProvider>(entity =>
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

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
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

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Picture)
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

            modelBuilder.Entity<ServiceProviderAdditionalQuestions>(entity =>
            {
                entity.Property(e => e.AdditionalQuestion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.AnswerOptionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AnswerOptions).HasMaxLength(4000);
            });

            modelBuilder.Entity<ServiceProviderAttachments>(entity =>
            {
                entity.Property(e => e.AttachmentFileDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AttachmentFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServiceProviderBooking>(entity =>
            {
                entity.Property(e => e.BookingDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FreeOrPaidBooking)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.MinimumAmount).HasColumnType("money");
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.Property(e => e.Service)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UnavailableDates>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UnavailableDate).HasColumnType("datetime");
            });
        }
    }
}
