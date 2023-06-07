using System;
using LostAndFound.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LostAndFound.Database.Data
{
    public partial class LostAndFoundContext : DbContext
    {
        public LostAndFoundContext()
        {
        }

        public LostAndFoundContext(DbContextOptions<LostAndFoundContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<ImageTable> ImageTables { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemFound> ItemFounds { get; set; }
        public virtual DbSet<ItemLost> ItemLosts { get; set; }
        public virtual DbSet<ItemMatch> ItemMatches { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=LostAndFound;User ID=sa;Password=master");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("County");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ImageTable>(entity =>
            {
                entity.ToTable("ImageTable");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageData).IsRequired();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ImageTables)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageTable_Item");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ContactEmail)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_City");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_County");

                entity.HasOne(d => d.ItemType)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Item_ItemType");
            });

            modelBuilder.Entity<ItemFound>(entity =>
            {
                entity.ToTable("ItemFound");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemFounds)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemFound_Item");
            });

            modelBuilder.Entity<ItemLost>(entity =>
            {
                entity.ToTable("ItemLost");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemLosts)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemLost_Item");
            });

            modelBuilder.Entity<ItemMatch>(entity =>
            {
                entity.ToTable("ItemMatch");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ItemFound)
                    .WithMany(p => p.ItemMatches)
                    .HasForeignKey(d => d.ItemFoundId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMatch_ItemFound");

                entity.HasOne(d => d.ItemLost)
                    .WithMany(p => p.ItemMatches)
                    .HasForeignKey(d => d.ItemLostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemMatch_ItemLost");
            });

            modelBuilder.Entity<ItemType>(entity =>
            {
                entity.ToTable("ItemType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
