﻿using System;
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
        public virtual DbSet<CustomerRegistration> CustomerRegistration { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<LookupTable> LookupTable { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<TradeAdditionalQuestions> TradeAdditionalQuestions { get; set; }
        public virtual DbSet<TradeMaster> TradeMaster { get; set; }
        public virtual DbSet<Tradesperson> Tradesperson { get; set; }
        public virtual DbSet<TradespersonAttachments> TradespersonAttachments { get; set; }
        public virtual DbSet<TradespersonBooking> TradespersonBooking { get; set; }
        public virtual DbSet<UnavailableDates> UnavailableDates { get; set; }

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

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.LoginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
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

            modelBuilder.Entity<TradespersonAttachments>(entity =>
            {
                entity.Property(e => e.AttachmentFileDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AttachmentFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TradespersonBooking>(entity =>
            {
                entity.Property(e => e.BookingDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FreeOrPaidBooking)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.MinimumAmount).HasColumnType("money");
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
