using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfPlatzi.Migrations
{
    /// <inheritdoc />
    public partial class BulkInsertInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)"
            );

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "description", "name", "weight" },
                values: new object[,]
                {
                    { new Guid("4d762b80-b91b-4234-b600-ff67d9a9cc04"), "Actividades personales", "Actividades personales", 50 },
                    { new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c1e"), "Actividades pendientes", "Actividades pendientes", 20 }
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "id", "category_id", "created_date", "description", "priority", "title" },
                values: new object[,]
                {
                    { new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c10"), new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c1e"), new DateTime(2023, 8, 24, 11, 40, 42, 128, DateTimeKind.Local).AddTicks(210), "Pago de servicios publicos", 1, "Pago de servicios publicos" },
                    { new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c11"), new Guid("4d762b80-b91b-4234-b600-ff67d9a9cc04"), new DateTime(2023, 8, 24, 11, 40, 42, 128, DateTimeKind.Local).AddTicks(222), null, 2, "Terminar el proyecto de api" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "id",
                keyValue: new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c10"));

            migrationBuilder.DeleteData(
                table: "tasks",
                keyColumn: "id",
                keyValue: new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c11"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("4d762b80-b91b-4234-b600-ff67d9a9cc04"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: new Guid("dbd467ca-5628-40ec-99a1-8bd95fc78c1e"));
        }
    }
}
