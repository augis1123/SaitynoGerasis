﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace SaitynoGerasis.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20221019183444_initial migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SaitynoGerasis.Data.Entities.pardavejas", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Adresas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Miestas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pavadinimas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Pardavejas");
                });

            modelBuilder.Entity("SaitynoGerasis.Data.Entities.perkamapreke", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("fk_PrekeId")
                        .HasColumnType("int");

                    b.Property<int>("fk_SaskaitaId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("perkamapreke");
                });

            modelBuilder.Entity("SaitynoGerasis.Data.Entities.preke", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Aprasymas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Kaina")
                        .HasColumnType("float");

                    b.Property<int>("Kiekis")
                        .HasColumnType("int");

                    b.Property<string>("Pavadinimas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_PardavejasId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Preke");
                });

            modelBuilder.Entity("SaitynoGerasis.Data.Entities.saskaita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Miestas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pavarde")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PirkimoData")
                        .HasColumnType("datetime2");

                    b.Property<string>("Vardas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Saskaita");
                });
#pragma warning restore 612, 618
        }
    }
}
