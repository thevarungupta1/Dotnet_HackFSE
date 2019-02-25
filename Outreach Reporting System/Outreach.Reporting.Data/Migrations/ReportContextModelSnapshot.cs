﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Outreach.Reporting.Data.Entities;

namespace Outreach.Reporting.Data.Migrations
{
    [DbContext(typeof(ReportContext))]
    partial class ReportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Outreach.Reporting.Data.Entities.UserRoles", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Role");

                    b.HasKey("ID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Outreach.Reporting.Data.Entities.Users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("RoleID");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Outreach.Reporting.Entity.Entities.Associates", b =>
                {
                    b.Property<int>("ID")
                        .HasMaxLength(6)
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.None);

                    b.Property<string>("BaseLocation")
                        .HasMaxLength(50);

                    b.Property<string>("BusinessUnit")
                        .HasMaxLength(50);

                    b.Property<string>("ContactNumber")
                        .HasMaxLength(20);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Designation")
                        .HasMaxLength(50);

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Associates");
                });

            modelBuilder.Entity("Outreach.Reporting.Entity.Entities.Configurations", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("ID");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("Outreach.Reporting.Entity.Entities.Enrollments", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssociateID");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("EventID")
                        .IsRequired();

                    b.Property<string>("IIEPCategory")
                        .HasMaxLength(100);

                    b.Property<bool>("IsPOC");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Status")
                        .HasMaxLength(50);

                    b.Property<decimal>("TravelHours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VolunteerHours")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("AssociateID");

                    b.HasIndex("EventID");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Outreach.Reporting.Entity.Entities.Events", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int?>("ActivityType");

                    b.Property<string>("Address")
                        .HasMaxLength(250);

                    b.Property<string>("BaseLocation")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Beneficiary")
                        .HasMaxLength(100);

                    b.Property<string>("Category")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(100);

                    b.Property<string>("CouncilName")
                        .HasMaxLength(100);

                    b.Property<string>("Country")
                        .HasMaxLength(100);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("LivesImpacted");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("PinCode")
                        .HasMaxLength(20);

                    b.Property<string>("Project")
                        .HasMaxLength(100);

                    b.Property<string>("State")
                        .HasMaxLength(100);

                    b.Property<string>("Status")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Outreach.Reporting.Data.Entities.Users", b =>
                {
                    b.HasOne("Outreach.Reporting.Data.Entities.UserRoles", "UserRoles")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Outreach.Reporting.Entity.Entities.Enrollments", b =>
                {
                    b.HasOne("Outreach.Reporting.Entity.Entities.Associates", "Associates")
                        .WithMany()
                        .HasForeignKey("AssociateID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Outreach.Reporting.Entity.Entities.Events", "Events")
                        .WithMany()
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
