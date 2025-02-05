﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Migrations
{
    [DbContext(typeof(VHSMovieRentalDBContext))]
    [Migration("20200223033910_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VHSMovieRentalAPI.Models.Movie", b =>
                {
                    b.Property<int>("MovieID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RentalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedUserID")
                        .HasColumnType("int");

                    b.HasKey("MovieID");

                    b.HasIndex("UpdatedUserID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieLike", b =>
                {
                    b.Property<int>("MovieLikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("MovieLikeID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("MovieLikes");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MoviePriceLog", b =>
                {
                    b.Property<int>("MoviePriceLogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<decimal>("RentalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedUserID")
                        .HasColumnType("int");

                    b.HasKey("MoviePriceLogID");

                    b.HasIndex("UpdatedUserID");

                    b.ToTable("MoviePriceLogs");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieRentalTerm", b =>
                {
                    b.Property<int>("MovieRentalTermID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("LateReturnCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RentalDays")
                        .HasColumnType("int");

                    b.HasKey("MovieRentalTermID");

                    b.ToTable("MovieRentalTerm");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieTransaction", b =>
                {
                    b.Property<int>("MovieTransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Branch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedUserID")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("MovieTransactionID");

                    b.HasIndex("UpdatedUserID");

                    b.HasIndex("UserID");

                    b.ToTable("MovieTransaction");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieTransactionDetail", b =>
                {
                    b.Property<int>("MovieTransactionDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.Property<int>("MovieRentalTermID")
                        .HasColumnType("int");

                    b.Property<int>("MovieTransactionID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Returned")
                        .HasColumnType("bit");

                    b.Property<int>("TransactionTypeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("UpdatedUserID")
                        .HasColumnType("int");

                    b.HasKey("MovieTransactionDetailID");

                    b.HasIndex("MovieID");

                    b.HasIndex("MovieRentalTermID");

                    b.HasIndex("MovieTransactionID");

                    b.HasIndex("TransactionTypeID");

                    b.HasIndex("UpdatedUserID");

                    b.ToTable("MovieTransactionDetail");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.TransactionType", b =>
                {
                    b.Property<int>("TransactionTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionTypeID");

                    b.ToTable("TransactionType");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.Movie", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.User", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieLike", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.Movie", "Movie")
                        .WithMany("MovieLikes")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MoviePriceLog", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.User", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieTransaction", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.User", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.MovieTransactionDetail", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.MovieRentalTerm", "MovieRentalTerm")
                        .WithMany()
                        .HasForeignKey("MovieRentalTermID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.MovieTransaction", null)
                        .WithMany("MovieTransactionDetails")
                        .HasForeignKey("MovieTransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.TransactionType", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VHSMovieRentalAPI.Models.User", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VHSMovieRentalAPI.Models.User", b =>
                {
                    b.HasOne("VHSMovieRentalAPI.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
