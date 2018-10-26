﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhatSAP.Models
{
    public partial class WhatSAPContext : DbContext
    {
        public WhatSAPContext()
        { }

        public WhatSAPContext(DbContextOptions<WhatSAPContext> options)
            : base(options)
        { }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:whatsap.database.windows.net,1433;Initial Catalog=WhatSAP;Persist Security Info=False;User ID=WhatsapAdmin;Password=Centennial@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Key)
                    .HasMaxLength(152)
                    .HasComputedColumnSql("([ActivityName]+CONVERT([nvarchar](24),[ActivityDate]))");

                entity.Property(e => e.Rate).HasColumnType("decimal(5, 1)");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Activity__Addres__74794A92");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Activity__Catego__73852659");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Activity)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Activity__Client__756D6ECB");
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PostalCode).IsRequired();

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnType("char(2)");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Booking__ClientI__531856C7");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Booking__Custome__5224328E");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK__Booking__Payment__55009F39");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Rate).HasColumnType("decimal(5, 1)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Comment__Custome__58D1301D");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Phone).IsRequired();
            });
        }
    }
}
