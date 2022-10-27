﻿// <auto-generated />
using System;
using FunctionApp2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FunctionApp2.Migrations
{
    [DbContext(typeof(PowerConsumptionContext))]
    [Migration("20221023213829_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FunctionApp2.PowerConsumption", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<double>("Consumption")
                        .HasColumnType("double precision");

                    b.Property<double>("Power")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("PowerConsumption");
                });
#pragma warning restore 612, 618
        }
    }
}