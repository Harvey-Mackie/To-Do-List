using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace To_Do_List_Library.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoList",
                columns: table => new
                {
                    ToDoListId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoList", x => x.ToDoListId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItem",
                columns: table => new
                {
                    ToDoItemId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Completed = table.Column<bool>(nullable: false),
                    ToDoListId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItem", x => x.ToDoItemId);
                    table.ForeignKey(
                        name: "FK_ToDoItem_ToDoList_ToDoListId",
                        column: x => x.ToDoListId,
                        principalTable: "ToDoList",
                        principalColumn: "ToDoListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ToDoList",
                columns: new[] { "ToDoListId", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name", "UserId" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Test To Do List", new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa") });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedBy", "CreatedDate", "Email", "FirstName", "LastModifiedBy", "LastModifiedDate", "LastName", "Password" },
                values: new object[] { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@test.com", "test", null, null, "test", "test" });

            migrationBuilder.InsertData(
                table: "ToDoItem",
                columns: new[] { "ToDoItemId", "Completed", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Title", "ToDoListId" },
                values: new object[] { new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"), false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "First Item on the list", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde") });

            migrationBuilder.InsertData(
                table: "ToDoItem",
                columns: new[] { "ToDoItemId", "Completed", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Title", "ToDoListId" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5ddd"), false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Second Item on the list", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde") });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_ToDoListId",
                table: "ToDoItem",
                column: "ToDoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ToDoList");
        }
    }
}
