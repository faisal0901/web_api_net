﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokonyadiaRestAPI.Repositories;

#nullable disable

namespace TokonyadiaRestAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TokonyadiaEF.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVarchar(100)")
                        .HasColumnName("description");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("NVarchar(50)")
                        .HasColumnName("product_name");

                    b.HasKey("Id");

                    b.ToTable("m_product");
                });

            modelBuilder.Entity("TokonyadiaEF.Entities.ProductPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<long>("Price")
                        .HasColumnType("bigint")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_id");

                    b.Property<int>("Stock")
                        .HasColumnType("int")
                        .HasColumnName("stock");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("store_id");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StoreId");

                    b.ToTable("m_product_price");
                });

            modelBuilder.Entity("TokonyadiaEF.Entities.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("NVarchar(100)")
                        .HasColumnName("address");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("NVarchar(14)")
                        .HasColumnName("phone_number");

                    b.Property<string>("SiupNumber")
                        .IsRequired()
                        .HasColumnType("NVarchar(9)")
                        .HasColumnName("siup_number");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("NVarchar(48)")
                        .HasColumnName("store_name");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("SiupNumber")
                        .IsUnique();

                    b.ToTable("m_store");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("NVarchar(100)")
                        .HasColumnName("address");

                    b.Property<string>("CustomerName")
                        .HasColumnType("NVarchar(48)")
                        .HasColumnName("customer_name");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("NVarchar(14)")
                        .HasColumnName("phone_number");

                    b.Property<Guid>("UserCredentialsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[phone_number] IS NOT NULL");

                    b.HasIndex("UserCredentialsId");

                    b.ToTable("m_customer");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("TransDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("trans_date");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("t_purchase");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.PurchaseDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("ProductPriceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("product_price_id");

                    b.Property<Guid?>("PurchaseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("purchase_id");

                    b.Property<int>("Qty")
                        .HasColumnType("int")
                        .HasColumnName("qty");

                    b.HasKey("Id");

                    b.HasIndex("ProductPriceId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("t_purchase_detail");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<int>("Erole")
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("m_role");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.UserCredential", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("m_user_credential");
                });

            modelBuilder.Entity("TokonyadiaEF.Entities.ProductPrice", b =>
                {
                    b.HasOne("TokonyadiaEF.Entities.Product", "Product")
                        .WithMany("ProductPrices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TokonyadiaEF.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Customer", b =>
                {
                    b.HasOne("TokonyadiaRestAPI.Entities.UserCredential", "UserCredentials")
                        .WithMany()
                        .HasForeignKey("UserCredentialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCredentials");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Purchase", b =>
                {
                    b.HasOne("TokonyadiaRestAPI.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.PurchaseDetail", b =>
                {
                    b.HasOne("TokonyadiaEF.Entities.ProductPrice", "ProductPrice")
                        .WithMany()
                        .HasForeignKey("ProductPriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TokonyadiaRestAPI.Entities.Purchase", "Purchase")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId");

                    b.Navigation("ProductPrice");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.UserCredential", b =>
                {
                    b.HasOne("TokonyadiaRestAPI.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TokonyadiaEF.Entities.Product", b =>
                {
                    b.Navigation("ProductPrices");
                });

            modelBuilder.Entity("TokonyadiaRestAPI.Entities.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
