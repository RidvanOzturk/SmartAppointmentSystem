﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartAppointmentSystem.Data;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    [DbContext(typeof(AppointmentContext))]
    [Migration("20250107170319_changedSomeConfOperate")]
    partial class changedSomeConfOperate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("ProfessionalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("Appointments", (string)null);
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Process", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("ProfessionalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("Processes", (string)null);
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfessionalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("Ratings", (string)null);
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.TimeSlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("AvailableFrom")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("AvailableTo")
                        .HasColumnType("time");

                    b.Property<Guid>("ProfessionalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfessionalId");

                    b.ToTable("TimeSlots", (string)null);
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Appointment", b =>
                {
                    b.HasOne("SmartAppointmentSystem.Data.Entities.User", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartAppointmentSystem.Data.Entities.User", "Professional")
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Process", b =>
                {
                    b.HasOne("SmartAppointmentSystem.Data.Entities.User", "Professional")
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Rating", b =>
                {
                    b.HasOne("SmartAppointmentSystem.Data.Entities.User", "Customer")
                        .WithMany("Ratings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartAppointmentSystem.Data.Entities.User", "Professional")
                        .WithMany()
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.TimeSlot", b =>
                {
                    b.HasOne("SmartAppointmentSystem.Data.Entities.Process", "Process")
                        .WithMany("TimeSlots")
                        .HasForeignKey("ProfessionalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Process");
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.Process", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("SmartAppointmentSystem.Data.Entities.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
