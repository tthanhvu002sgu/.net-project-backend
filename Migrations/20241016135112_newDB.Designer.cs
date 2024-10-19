﻿// <auto-generated />
using System;
using DoAn_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DoAn_API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20241016135112_newDB")]
    partial class newDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DoAn_API.Data.Appointment", b =>
                {
                    b.Property<int>("appointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("appointmentId"));

                    b.Property<DateTime>("appointmentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("appointmentDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("appointmentTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<int>("patientId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("appointmentId");

                    b.HasIndex("doctorId");

                    b.HasIndex("patientId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("DoAn_API.Data.Doctor", b =>
                {
                    b.Property<int>("doctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("doctorId"));

                    b.Property<double>("bookingFee")
                        .HasColumnType("double");

                    b.Property<string>("degree")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("doctorAbout")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("doctorName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("experience")
                        .HasColumnType("double");

                    b.HasKey("doctorId");

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("DoAn_API.Data.Patient", b =>
                {
                    b.Property<int>("patientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("patientId"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("patientDob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("patientName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("patientId");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("DoAn_API.Data.Schedule", b =>
                {
                    b.Property<int>("scheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("scheduleId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("DoAn_API.Data.Specialization", b =>
                {
                    b.Property<int>("specializationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("specializationId"));

                    b.Property<string>("specialization")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("specializationId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("DoctorSpecialization", b =>
                {
                    b.Property<int>("doctorId")
                        .HasColumnType("int");

                    b.Property<int>("specializationId")
                        .HasColumnType("int");

                    b.HasKey("doctorId", "specializationId");

                    b.HasIndex("specializationId");

                    b.ToTable("DoctorSpecialization", (string)null);
                });

            modelBuilder.Entity("DoAn_API.Data.Appointment", b =>
                {
                    b.HasOne("DoAn_API.Data.Doctor", "doctor")
                        .WithMany("appointments")
                        .HasForeignKey("doctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAn_API.Data.Patient", "patient")
                        .WithMany("appointments")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("DoAn_API.Data.Schedule", b =>
                {
                    b.HasOne("DoAn_API.Data.Appointment", "appointment")
                        .WithOne("schedule")
                        .HasForeignKey("DoAn_API.Data.Schedule", "scheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAn_API.Data.Doctor", "doctor")
                        .WithMany("schedules")
                        .HasForeignKey("scheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appointment");

                    b.Navigation("doctor");
                });

            modelBuilder.Entity("DoctorSpecialization", b =>
                {
                    b.HasOne("DoAn_API.Data.Doctor", null)
                        .WithMany()
                        .HasForeignKey("doctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoAn_API.Data.Specialization", null)
                        .WithMany()
                        .HasForeignKey("specializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DoAn_API.Data.Appointment", b =>
                {
                    b.Navigation("schedule")
                        .IsRequired();
                });

            modelBuilder.Entity("DoAn_API.Data.Doctor", b =>
                {
                    b.Navigation("appointments");

                    b.Navigation("schedules");
                });

            modelBuilder.Entity("DoAn_API.Data.Patient", b =>
                {
                    b.Navigation("appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
