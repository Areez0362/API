﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ItemDBContext))]
    [Migration("20221207164329_Fifth Migration")]
    partial class FifthMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Entities.Items", b =>
                {
                    b.Property<Guid>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("API.Models.Entities.Receipt", b =>
                {
                    b.Property<Guid>("ReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalAmount")
                        .HasColumnType("int");

                    b.HasKey("ReceiptID");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("API.Models.Entities.SoldItems", b =>
                {
                    b.Property<Guid>("ItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ReceiptID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ItemID", "ReceiptID");

                    b.HasIndex("ReceiptID");

                    b.ToTable("SoldItems");
                });

            modelBuilder.Entity("API.Models.Entities.SoldItems", b =>
                {
                    b.HasOne("API.Models.Entities.Receipt", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });
#pragma warning restore 612, 618
        }
    }
}
