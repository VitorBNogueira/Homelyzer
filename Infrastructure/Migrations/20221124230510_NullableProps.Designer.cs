﻿// <auto-generated />
using System;
using Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(HomelyzerDBContext))]
    [Migration("20221124230510_NullableProps")]
    partial class NullableProps
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Advert", b =>
                {
                    b.Property<int>("AdvertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IncludesBills")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("MeetingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Score")
                        .HasColumnType("float");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdvertId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Property<int>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OwnerId"));

                    b.Property<string>("EmailContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneContact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("Domain.Entities.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PictureId"));

                    b.Property<int>("AdvertId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PictureId");

                    b.HasIndex("AdvertId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Domain.Entities.Advert", b =>
                {
                    b.HasOne("Domain.Entities.Owner", "Owner")
                        .WithMany("Adverts")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.Picture", b =>
                {
                    b.HasOne("Domain.Entities.Advert", "Advert")
                        .WithMany("Pictures")
                        .HasForeignKey("AdvertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advert");
                });

            modelBuilder.Entity("Domain.Entities.Advert", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Domain.Entities.Owner", b =>
                {
                    b.Navigation("Adverts");
                });
#pragma warning restore 612, 618
        }
    }
}
