﻿// <auto-generated />
using System;
using ITKT_PROJEKTAS.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITKT_PROJEKTAS.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Paslauga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Paslauga");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("PictureBytes")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("PictureFormat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RouteRouteId")
                        .HasColumnType("int");

                    b.Property<int?>("UserUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteRouteId");

                    b.HasIndex("UserUserId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Boat")
                        .HasColumnType("int");

                    b.Property<double>("Discount")
                        .HasColumnType("double");

                    b.Property<int>("PersonCount")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<double>("ReservationCost")
                        .HasColumnType("double");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("Difficulity")
                        .HasColumnType("int");

                    b.Property<double>("Lattitude")
                        .HasColumnType("double");

                    b.Property<double>("Length")
                        .HasColumnType("double");

                    b.Property<double>("Longtitude")
                        .HasColumnType("double");

                    b.Property<int>("MaxPeople")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<double>("PricePerPerson")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Route");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PaslaugaReservation", b =>
                {
                    b.Property<int>("PaslaugaId")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("PaslaugaId", "ReservationId");

                    b.HasIndex("ReservationId");

                    b.ToTable("PaslaugaReservation");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Picture", b =>
                {
                    b.HasOne("ITKT_PROJEKTAS.Entities.Route", "RouteRoute")
                        .WithMany("Pictures")
                        .HasForeignKey("RouteRouteId");

                    b.HasOne("ITKT_PROJEKTAS.Entities.User", "UserUser")
                        .WithMany()
                        .HasForeignKey("UserUserId");

                    b.Navigation("RouteRoute");

                    b.Navigation("UserUser");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Reservation", b =>
                {
                    b.HasOne("ITKT_PROJEKTAS.Entities.Route", "Route")
                        .WithOne("Reservation")
                        .HasForeignKey("ITKT_PROJEKTAS.Entities.Reservation", "RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITKT_PROJEKTAS.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PaslaugaReservation", b =>
                {
                    b.HasOne("ITKT_PROJEKTAS.Entities.Paslauga", null)
                        .WithMany()
                        .HasForeignKey("PaslaugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITKT_PROJEKTAS.Entities.Reservation", null)
                        .WithMany()
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.Route", b =>
                {
                    b.Navigation("Pictures");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("ITKT_PROJEKTAS.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
