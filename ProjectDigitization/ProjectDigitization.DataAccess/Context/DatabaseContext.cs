using Microsoft.EntityFrameworkCore;
using ProjectDIgitization.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ProjectDigitization.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)   { }

        public DbSet<Products> Products { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<UserTypes> UserTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Products>(b =>
            //{
            //    b.HasKey(p => p.Id);
            //    b.Property(p => p.ProductName).HasMaxLength(100).IsRequired();
            //    b.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //    b.Property(p => p.Discount).HasColumnType("decimal(5,2)");
            //});

            //modelBuilder.Entity<UserTypes>(b =>
            //{
            //    b.HasKey(ut => ut.Id);
            //    b.Property(ut => ut.Name).HasMaxLength(50).IsRequired();
            //});

            //modelBuilder.Entity<Users>(b =>
            //{
            //    b.HasKey(u => u.Id);
            //    b.Property(u => u.UserId).HasMaxLength(50).IsRequired();
            //    b.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            //    b.HasOne(u => u.UserType)
            //     .WithMany(ut => ut.Users)
            //     .HasForeignKey(u => u.UserTypeId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //});
        }
    }
}
