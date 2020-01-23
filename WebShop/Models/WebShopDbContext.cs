﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebShop.Models
{
    public partial class WebShopDbContext : DbContext
    {
        public WebShopDbContext()
        {
        }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasIndex(e => new { e.PurchaseOrderId, e.ProductId })
                    .HasName("UC_Product")
                    .IsUnique();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item__ProductId__0F624AF8");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item__PurchaseOr__0E6E26BF");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.PersonId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__0A9D95DB");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.DateOfOrder).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PurchaseO__Perso__04E4BC85");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}