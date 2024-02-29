﻿// <auto-generated />
using System;
using Bono.Employees.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bono.Employees.Infrastructure.Data.Migrations
{
    [DbContext(typeof(BonoEmployeeContext))]
    partial class BonoEmployeeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bono.Employees.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 4, 18, 33, 52, 588, DateTimeKind.Local).AddTicks(7379));

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.EmployeeType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9246));

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmployeeType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2d9d8cf3-80e7-4e0a-b6a4-544b5f169db5"),
                            DateCreated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(5406),
                            DateUpdated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(5613),
                            IsDeleted = false,
                            Type = "Standard"
                        },
                        new
                        {
                            Id = new Guid("a775b466-e19d-424c-b4f8-7d2686f78f06"),
                            DateCreated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6508),
                            DateUpdated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6515),
                            IsDeleted = false,
                            Type = "SaleEmployee"
                        },
                        new
                        {
                            Id = new Guid("0a1707e4-9740-4678-8c88-3eee452f187c"),
                            DateCreated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6518),
                            DateUpdated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6519),
                            IsDeleted = false,
                            Type = "PurchaseEmployee"
                        },
                        new
                        {
                            Id = new Guid("5dec9c99-ec79-4bcf-bd8f-651b88a90a4f"),
                            DateCreated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6522),
                            DateUpdated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6523),
                            IsDeleted = false,
                            Type = "TransferEmployee"
                        },
                        new
                        {
                            Id = new Guid("133ab82e-eb1d-4490-82f9-28809aade78b"),
                            DateCreated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6525),
                            DateUpdated = new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6527),
                            IsDeleted = false,
                            Type = "ReturnEmployee"
                        });
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9389));

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9501));

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LockoutEndDateUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("bono@teste");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ad9a02ee-e522-4b8a-844f-50cb95d076ba"),
                            AccessFailedCount = 0,
                            Cpf = "123.456.456-56",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "richiebono@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Richard Bono",
                            IsDeleted = false,
                            LastName = "Oliveira",
                            LockoutEnabled = false,
                            Password = "23D42F5F3F66498B2C8FF4C20B8C5AC826E47146",
                            PhoneNumber = "+55 11-98547-3851",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "Richard Bono"
                        });
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9656));

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.Employee", b =>
                {
                    b.HasOne("Bono.Employees.Domain.Entities.EmployeeType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.HasOne("Bono.Employees.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Bono.Employees.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Bono.Employees.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Bono.Employees.Domain.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
