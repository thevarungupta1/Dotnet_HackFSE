using Microsoft.EntityFrameworkCore;
using Outreach.Reporting.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Outreach.Reporting.Data.Entities
{
    public class ReportDBContext : DbContext
    {
        public ReportDBContext(DbContextOptions<ReportDBContext> options) : base(options)
        {
        }

        public DbSet<Associate> Associates { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PointOfContact> PointOfContact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Associate>()
                   .Property(c => c.ID)
                   .ValueGeneratedNever()
                   .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

            modelBuilder.Entity<UserRole>().HasData(
                                            new UserRole { ID = 1, Role = "Admin", Description = "administrator", CreatedBy = "Admin", CreatedOn = DateTime.Now },
                                            new UserRole { ID = 2, Role = "PMO", Description = "pmo", CreatedBy = "Admin", CreatedOn = DateTime.Now },
                                            new UserRole { ID = 3, Role = "POC", Description = "point of contact", CreatedBy = "Admin", CreatedOn = DateTime.Now }
                                        );
        }

    }
}
