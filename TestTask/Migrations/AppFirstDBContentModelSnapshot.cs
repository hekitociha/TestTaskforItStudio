﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.Data;

#nullable disable

namespace TestTask.Migrations
{
    [DbContext(typeof(AppFirstDBContent))]
    partial class AppFirstDBContentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestTask.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isCopied")
                        .HasColumnType("bit");

                    b.Property<DateTime>("uploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
