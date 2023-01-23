﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopway.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Master");

            migrationBuilder.EnsureSchema(
                name: "Shopway");

            migrationBuilder.EnsureSchema(
                name: "Outbox");

            migrationBuilder.EnsureSchema(
                name: "Workflow");

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "Outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProcessedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageConsumer",
                schema: "Outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageConsumer", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Shopway",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "VarChar(10)", nullable: false),
                    OccurredOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    OrderId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "TinyInt", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VarChar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Shopway",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Revision = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    UomCode = table.Column<string>(type: "VarChar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "TinyInt", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VarChar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                schema: "Shopway",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Stars = table.Column<byte>(type: "TinyInt", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Shopway",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Master",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "TinyInt", nullable: false),
                    PermissionId = table.Column<byte>(type: "TinyInt", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Master",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Building = table.Column<int>(type: "int", maxLength: 1000, nullable: false),
                    Flat = table.Column<int>(type: "int", maxLength: 1000, nullable: true),
                    PersonId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Rank = table.Column<string>(type: "VarChar(8)", nullable: false, defaultValue: "Standard")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Shopway",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "VarChar(10)", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Master",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Shopway",
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Shopway",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    HireDate = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    ManagerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Employee_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "Master",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "VarChar(7)", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    EmployeeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Master",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkItem",
                schema: "Workflow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Priority = table.Column<byte>(type: "TinyInt", nullable: false, defaultValue: (byte)0),
                    Status = table.Column<string>(type: "VarChar(10)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    StoryPoints = table.Column<byte>(type: "TinyInt", nullable: false, defaultValue: (byte)1),
                    EmployeeId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItem_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Master",
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Master",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: false),
                    UpdatedOn = table.Column<DateTimeOffset>(type: "DateTimeOffset(2)", nullable: true),
                    PasswordHash = table.Column<string>(type: "NChar(514)", nullable: false),
                    PersonId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Master",
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                schema: "Master",
                columns: table => new
                {
                    RoleId = table.Column<byte>(type: "TinyInt", nullable: false),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Master",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Read" },
                    { (byte)2, "Create" },
                    { (byte)3, "Update" },
                    { (byte)4, "Delete" },
                    { (byte)5, "CRUD_Review" }
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Customer" },
                    { (byte)2, "Employee" },
                    { (byte)3, "Manager" },
                    { (byte)4, "Administrator" }
                });

            migrationBuilder.InsertData(
                schema: "Master",
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { (byte)1, (byte)1 },
                    { (byte)5, (byte)1 },
                    { (byte)1, (byte)2 },
                    { (byte)2, (byte)2 },
                    { (byte)1, (byte)3 },
                    { (byte)2, (byte)3 },
                    { (byte)3, (byte)3 },
                    { (byte)1, (byte)4 },
                    { (byte)2, (byte)4 },
                    { (byte)3, (byte)4 },
                    { (byte)4, (byte)4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                schema: "Master",
                table: "Address",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ManagerId",
                schema: "Master",
                table: "Employee",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "Shopway",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentId",
                schema: "Shopway",
                table: "Order",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductId_Status",
                schema: "Shopway",
                table: "Order",
                columns: new[] { "ProductId", "Status" },
                filter: "Status IN ('New', 'InProgress')");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrderId_Status",
                schema: "Shopway",
                table: "Payment",
                columns: new[] { "OrderId", "Status" },
                filter: "Status <> 'Rejected'");

            migrationBuilder.CreateIndex(
                name: "IX_Person_EmployeeId",
                schema: "Master",
                table: "Person",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "UX_Person_Email",
                schema: "Master",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                schema: "Shopway",
                table: "Review",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "Master",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UserId",
                schema: "Master",
                table: "RoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PersonId",
                schema: "Master",
                table: "User",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UX_User_Email",
                schema: "Master",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Username_Email",
                schema: "Master",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItem_EmployeeId",
                schema: "Workflow",
                table: "WorkItem",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Person_PersonId",
                schema: "Master",
                table: "Address",
                column: "PersonId",
                principalSchema: "Master",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Person_Id",
                schema: "Master",
                table: "Customer",
                column: "Id",
                principalSchema: "Master",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Person_Id",
                schema: "Master",
                table: "Employee",
                column: "Id",
                principalSchema: "Master",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Person_Id",
                schema: "Master",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Shopway");

            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumer",
                schema: "Outbox");

            migrationBuilder.DropTable(
                name: "Review",
                schema: "Shopway");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "RoleUser",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "WorkItem",
                schema: "Workflow");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Shopway");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Shopway");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Master");
        }
    }
}
