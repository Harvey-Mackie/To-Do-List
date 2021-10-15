﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using To_Do_List_Library.Persistence.Configuration;

namespace To_Do_List_Library.Persistence.Migrations
{
    [DbContext(typeof(ToDoDbContext))]
    [Migration("20211013060457_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("To_Do_List_Library.Core.Entities.ToDoItem", b =>
                {
                    b.Property<Guid>("ToDoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ToDoListId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ToDoItemId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoItem");

                    b.HasData(
                        new
                        {
                            ToDoItemId = new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"),
                            Completed = false,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "First Item on the list",
                            ToDoListId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde")
                        },
                        new
                        {
                            ToDoItemId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5ddd"),
                            Completed = false,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Second Item on the list",
                            ToDoListId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde")
                        });
                });

            modelBuilder.Entity("To_Do_List_Library.Core.Entities.ToDoList", b =>
                {
                    b.Property<Guid>("ToDoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ToDoListId");

                    b.ToTable("ToDoList");

                    b.HasData(
                        new
                        {
                            ToDoListId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Test To Do List",
                            UserId = new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa")
                        });
                });

            modelBuilder.Entity("To_Do_List_Library.Core.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "test@test.com",
                            FirstName = "test",
                            LastName = "test",
                            Password = "test"
                        });
                });

            modelBuilder.Entity("To_Do_List_Library.Core.Entities.ToDoItem", b =>
                {
                    b.HasOne("To_Do_List_Library.Core.Entities.ToDoList", null)
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
