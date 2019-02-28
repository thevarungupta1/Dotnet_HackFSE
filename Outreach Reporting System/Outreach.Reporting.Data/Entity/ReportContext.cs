using Microsoft.EntityFrameworkCore;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
        }

        public DbSet<Associates> Associates { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Enrollments> Enrollments { get; set; }
       // public DbSet<Locations> Locations { get; set; }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }
        public DbSet<Configurations> Configurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Associates>().ToTable("Associates");
            modelBuilder.Entity<Events>().ToTable("Events");
            modelBuilder.Entity<Enrollments>().ToTable("Enrollments");
            //modelBuilder.Entity<Locations>().ToTable("Locations");
            modelBuilder.Entity<ApplicationUsers>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Configurations>().ToTable("Configurations");

            modelBuilder.Entity<Associates>()
                   .Property(c => c.ID)
                   .ValueGeneratedNever()
                   .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
        }

    }
}
