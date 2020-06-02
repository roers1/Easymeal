﻿// <auto-generated />
using System;
using EF_SQLServer_DishDataImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_SQLServer_DishDataImpl.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191202135357_Removed active field from dish model")]
    partial class Removedactivefieldfromdishmodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfTheWeek");

                    b.HasKey("DayId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.DayDish", b =>
                {
                    b.Property<int>("DayId");

                    b.Property<int>("DishId");

                    b.HasKey("DayId", "DishId");

                    b.HasIndex("DishId");

                    b.ToTable("DayDishes");
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.Dish", b =>
                {
                    b.Property<int>("DishId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Dessert");

                    b.Property<bool>("Diabetes");

                    b.Property<bool>("Glutes");

                    b.Property<byte[]>("Image");

                    b.Property<bool>("MainDish");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Salt");

                    b.Property<bool>("Starter");

                    b.HasKey("DishId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.Weekplanning", b =>
                {
                    b.Property<int>("WeekplanningId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("WeekplanningId");

                    b.ToTable("Weekplannings");
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.WeekplanningDay", b =>
                {
                    b.Property<int>("WeekplanningId");

                    b.Property<int>("DayId");

                    b.HasKey("WeekplanningId", "DayId");

                    b.HasIndex("DayId");

                    b.ToTable("WeekplanningDays");
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.DayDish", b =>
                {
                    b.HasOne("EasyMealCore.DomainModel.Cook.Day", "Day")
                        .WithMany("DayDishes")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyMealCore.DomainModel.Cook.Dish", "Dish")
                        .WithMany("DayDishes")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMealCore.DomainModel.Cook.WeekplanningDay", b =>
                {
                    b.HasOne("EasyMealCore.DomainModel.Cook.Day", "Day")
                        .WithMany("weekplanningDays")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyMealCore.DomainModel.Cook.Weekplanning", "Weekplanning")
                        .WithMany("weekplanningDays")
                        .HasForeignKey("WeekplanningId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
