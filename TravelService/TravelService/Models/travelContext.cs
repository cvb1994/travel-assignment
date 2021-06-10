﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TravelService.Models
{
    public partial class travelContext : DbContext
    {

        public travelContext(DbContextOptions<travelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Comment__ImageId__02084FDA");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK__Comment__PlaceId__01142BA1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__UserId__02FC7413");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__Images__7516F70C073E44B9");

                entity.Property(e => e.ImageLink)
                    .IsRequired()
                    .HasColumnName("Image_link")
                    .HasMaxLength(40);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK__Images__PlaceId__7E37BEF6");
            });

            modelBuilder.Entity<Places>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__Places__D5222B6EC34995C2");

                entity.Property(e => e.Info)
                    .IsRequired()
                    .HasColumnName("info")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Places__UserId__7B5B524B");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Rating__ImageId__06CD04F7");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK__Rating__PlaceId__05D8E0BE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Rating__UserId__07C12930");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C9CC29D19");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__787EE5A0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
