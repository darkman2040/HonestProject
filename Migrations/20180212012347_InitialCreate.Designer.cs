﻿// <auto-generated />
using HonestProject.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HonestProject.Migrations
{
    [DbContext(typeof(HonestProjectContext))]
    [Migration("20180212012347_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HonestProject.DataModels.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("HonestProject.DataModels.Site", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HoursPerDay");

                    b.Property<bool>("IncludeWeekends");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("PublicIdentifier");

                    b.Property<string>("UniqueSiteId")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Site");
                });

            modelBuilder.Entity("HonestProject.DataModels.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("HonestProject.DataModels.TimeEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hours");

                    b.Property<int>("Minutes");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("TimeEntry");
                });

            modelBuilder.Entity("HonestProject.DataModels.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<Guid>("PublicIdentifier");

                    b.Property<int?>("RoleID");

                    b.Property<int>("SiteID");

                    b.Property<int?>("TeamID");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("SiteID");

                    b.HasIndex("TeamID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HonestProject.DataModels.TimeEntry", b =>
                {
                    b.HasOne("HonestProject.DataModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HonestProject.DataModels.User", b =>
                {
                    b.HasOne("HonestProject.DataModels.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.HasOne("HonestProject.DataModels.Site", "Site")
                        .WithMany()
                        .HasForeignKey("SiteID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HonestProject.DataModels.Team", "Team")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamID");
                });
#pragma warning restore 612, 618
        }
    }
}
