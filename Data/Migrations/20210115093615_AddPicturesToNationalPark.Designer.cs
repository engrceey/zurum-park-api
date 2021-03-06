﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZurumPark.Data;

namespace ZurumPark.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210115093615_AddPicturesToNationalPark")]
    partial class AddPicturesToNationalPark
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ZurumPark.Entities.NationalPark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Established")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Pictures")
                        .HasColumnType("BLOB");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NationalParks");
                });

            modelBuilder.Entity("ZurumPark.Entities.Trail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<int>("Difficulty")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Distance")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NationalParkId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NationalParkId");

                    b.ToTable("Trails");
                });

            modelBuilder.Entity("ZurumPark.Entities.Trail", b =>
                {
                    b.HasOne("ZurumPark.Entities.NationalPark", "NationalPark")
                        .WithMany()
                        .HasForeignKey("NationalParkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NationalPark");
                });
#pragma warning restore 612, 618
        }
    }
}
