﻿// <auto-generated />
using System;
using CareerBoostAI.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    [DbContext(typeof(CareerBoostReadDbContext))]
    [Migration("20250131035714_AddIndexToCandidateEmailColumn")]
    partial class AddIndexToCandidateEmailColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.CandidateReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("CandidateEmail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateEmail")
                        .IsUnique();

                    b.ToTable("Cvs");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.EducationReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CvId")
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id", "CvId");

                    b.HasIndex("CvId");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.ExperienceReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CvId")
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id", "CvId");

                    b.HasIndex("CvId");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.LanguageReadModel", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("CvId")
                        .HasColumnType("char(36)");

                    b.HasKey("Name");

                    b.HasIndex("CvId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.SkillReadModel", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("CvId")
                        .HasColumnType("char(36)");

                    b.HasKey("Name");

                    b.HasIndex("CvId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.UploadReadModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CandidateEmail")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StorageAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StorageMedium")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CandidateEmail");

                    b.ToTable("UploadReadModel");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CandidateReadModel", "CandidateReadModel")
                        .WithOne("CvReadModel")
                        .HasForeignKey("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", "CandidateEmail")
                        .HasPrincipalKey("CareerBoostAI.Infrastructure.EF.Models.CandidateReadModel", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CandidateReadModel");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.EducationReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", "CvReadModel")
                        .WithMany("Educations")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CvReadModel");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.ExperienceReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", "CvReadModel")
                        .WithMany("Experiences")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CvReadModel");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.LanguageReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", "Cv")
                        .WithMany("Languages")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.SkillReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", "Cv")
                        .WithMany("Skills")
                        .HasForeignKey("CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cv");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.UploadReadModel", b =>
                {
                    b.HasOne("CareerBoostAI.Infrastructure.EF.Models.CandidateReadModel", "CandidateReadModel")
                        .WithMany("Uploads")
                        .HasForeignKey("CandidateEmail")
                        .HasPrincipalKey("Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CandidateReadModel");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.CandidateReadModel", b =>
                {
                    b.Navigation("CvReadModel")
                        .IsRequired();

                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("CareerBoostAI.Infrastructure.EF.Models.CvReadModel", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("Languages");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
