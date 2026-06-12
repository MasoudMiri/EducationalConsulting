using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalConsulting.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 7, 9, 33, 737, DateTimeKind.Local).AddTicks(8309));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 7, 9, 33, 737, DateTimeKind.Local).AddTicks(8313));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 7, 9, 33, 737, DateTimeKind.Local).AddTicks(8316));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 7, 9, 33, 737, DateTimeKind.Local).AddTicks(8319));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 5, 17, 50, 764, DateTimeKind.Local).AddTicks(1234));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 5, 17, 50, 764, DateTimeKind.Local).AddTicks(1238));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 5, 17, 50, 764, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreateDate",
                value: new DateTime(2026, 6, 12, 5, 17, 50, 764, DateTimeKind.Local).AddTicks(1244));
        }
    }
}
