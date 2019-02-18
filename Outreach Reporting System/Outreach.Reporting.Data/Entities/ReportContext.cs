using Microsoft.EntityFrameworkCore;
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
        public DbSet<AssociatesEnrolledToEvents> AssociatesEnrolledToEvents { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Configurations> Configurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Associates>().ToTable("Associates");
            modelBuilder.Entity<Events>().ToTable("Events");
            modelBuilder.Entity<AssociatesEnrolledToEvents>().ToTable("AssociatesEnrolledToEvents");
            modelBuilder.Entity<Locations>().ToTable("Locations");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Configurations>().ToTable("Configurations");

            modelBuilder.Entity<Associates>()
                   .Property(c => c.ID)
                   .ValueGeneratedNever()
                   .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);
        }

    }
}
