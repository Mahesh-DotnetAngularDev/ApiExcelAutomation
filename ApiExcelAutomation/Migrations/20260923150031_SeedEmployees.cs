using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiExcelAutomation.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "Department", "Email", "JoiningDate", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "IT", "rahul.sharma@example.com", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rahul Sharma", 65000m },
                    { 2, "HR", "priya.das@example.com", new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Priya Das", 58000m },
                    { 3, "Finance", "amit.kumar@example.com", new DateTime(2022, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amit Kumar", 72000m },
                    { 4, "IT", "sneha.patel@example.com", new DateTime(2021, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sneha Patel", 81000m },
                    { 5, "Sales", "arjun.singh@example.com", new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arjun Singh", 55000m },
                    { 6, "Finance", "neha.roy@example.com", new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neha Roy", 69000m },
                    { 7, "IT", "vikash.das@example.com", new DateTime(2020, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vikash Das", 92000m },
                    { 8, "HR", "anjali.mishra@example.com", new DateTime(2022, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anjali Mishra", 62000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
