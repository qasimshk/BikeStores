﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bs.inventory.infrastructure.Persistence.Context;

namespace bs.inventory.infrastructure.Persistence.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20210411131634_UpdateProductColumn")]
    partial class UpdateProductColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bs.inventory.domain.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("BasketRef")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("_basketId")
                        .HasColumnType("int")
                        .HasColumnName("BasketId");

                    b.Property<int>("_productId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("_basketId");

                    b.ToTable("BasketItem");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.InventoryStatus", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BasketRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentState")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FailedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("CorrelationId");

                    b.ToTable("InventoryStatus");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<int>("ModelYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("ProductRef")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Reference")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("_brandId")
                        .HasColumnType("int")
                        .HasColumnName("BrandId");

                    b.Property<int>("_categoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.HasKey("Id");

                    b.HasIndex("_brandId");

                    b.HasIndex("_categoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("_productId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.Property<int>("_stockIn")
                        .HasColumnType("int")
                        .HasColumnName("StockIn");

                    b.Property<int>("_stockOut")
                        .HasColumnType("int")
                        .HasColumnName("StockOut");

                    b.Property<int>("_storeId")
                        .HasColumnType("int")
                        .HasColumnName("StoreId");

                    b.HasKey("Id");

                    b.HasIndex("_productId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Phone")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.BasketItem", b =>
                {
                    b.HasOne("bs.inventory.domain.Entities.Basket", "Basket")
                        .WithMany("BasketItems")
                        .HasForeignKey("_basketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Basket");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Product", b =>
                {
                    b.HasOne("bs.inventory.domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("_brandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bs.inventory.domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("_categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Stock", b =>
                {
                    b.HasOne("bs.inventory.domain.Entities.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("_productId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Store", b =>
                {
                    b.OwnsOne("bs.inventory.domain.Entities.Address", "StoreAddress", b1 =>
                        {
                            b1.Property<int>("StoreId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("PostCode")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("StoreId");

                            b1.ToTable("Store");

                            b1.WithOwner()
                                .HasForeignKey("StoreId");
                        });

                    b.Navigation("StoreAddress");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Basket", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("bs.inventory.domain.Entities.Product", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
