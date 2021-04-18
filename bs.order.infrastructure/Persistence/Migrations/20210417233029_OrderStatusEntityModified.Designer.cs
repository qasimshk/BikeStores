﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bs.order.infrastructure.Persistence.Context;

namespace bs.order.infrastructure.Persistence.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20210417233029_OrderStatusEntityModified")]
    partial class OrderStatusEntityModified
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bs.order.domain.Entities.CardDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CardType")
                        .HasColumnType("int");

                    b.Property<string>("_cardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CardNumber");

                    b.Property<int>("_customerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<DateTime>("_expiration")
                        .HasColumnType("datetime2")
                        .HasColumnName("Expiration");

                    b.Property<int>("_securityNumber")
                        .HasColumnType("int")
                        .HasColumnName("SecurityNumber");

                    b.HasKey("Id");

                    b.HasIndex("_customerId");

                    b.ToTable("CardDetail");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Consent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ContactByCall")
                        .HasColumnType("bit");

                    b.Property<bool>("ContactByEmail")
                        .HasColumnType("bit");

                    b.Property<bool>("ContactByPost")
                        .HasColumnType("bit");

                    b.Property<bool>("ContactByText")
                        .HasColumnType("bit");

                    b.Property<int>("_customerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("_customerId")
                        .IsUnique();

                    b.ToTable("Consent");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("DateOfBirth");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CancelledOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeliveredOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReasonOfCancellation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("_customerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.Property<int>("_paymentId")
                        .HasColumnType("int")
                        .HasColumnName("PaymentId");

                    b.HasKey("Id");

                    b.HasIndex("_customerId");

                    b.HasIndex("_paymentId")
                        .IsUnique();

                    b.ToTable("Order");
                });

            modelBuilder.Entity("bs.order.domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("IndividualPrice")
                        .HasColumnType("float");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("_orderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("_orderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("bs.order.domain.Entities.OrderState", b =>
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

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FailedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("JsonOrderItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionRef")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CorrelationId");

                    b.ToTable("OrderState");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<Guid>("PaymentRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RefundedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("TransactionStatus");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("_cardDetailId")
                        .HasColumnType("int")
                        .HasColumnName("CardDetailId");

                    b.Property<int>("_customerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("_cardDetailId");

                    b.HasIndex("_customerId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("bs.order.domain.Entities.CardDetail", b =>
                {
                    b.HasOne("bs.order.domain.Entities.Customer", "Customer")
                        .WithMany("CardDetails")
                        .HasForeignKey("_customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Consent", b =>
                {
                    b.HasOne("bs.order.domain.Entities.Customer", "Customer")
                        .WithOne("Consents")
                        .HasForeignKey("bs.order.domain.Entities.Consent", "_customerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Customer", b =>
                {
                    b.OwnsOne("bs.order.domain.Entities.Address", "BillingAddress", b1 =>
                        {
                            b1.Property<int>("CustomerId")
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

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("BillingAddress");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Order", b =>
                {
                    b.HasOne("bs.order.domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("_customerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("bs.order.domain.Entities.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("bs.order.domain.Entities.Order", "_paymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("bs.order.domain.Entities.Address", "DeliveryAddress", b1 =>
                        {
                            b1.Property<int>("OrderId")
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

                            b1.HasKey("OrderId");

                            b1.ToTable("Order");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Customer");

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("bs.order.domain.Entities.OrderItem", b =>
                {
                    b.HasOne("bs.order.domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("_orderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Payment", b =>
                {
                    b.HasOne("bs.order.domain.Entities.CardDetail", "CardDetail")
                        .WithMany("Payments")
                        .HasForeignKey("_cardDetailId");

                    b.HasOne("bs.order.domain.Entities.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("_customerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CardDetail");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("bs.order.domain.Entities.CardDetail", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Customer", b =>
                {
                    b.Navigation("CardDetails");

                    b.Navigation("Consents");

                    b.Navigation("Orders");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("bs.order.domain.Entities.Payment", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
