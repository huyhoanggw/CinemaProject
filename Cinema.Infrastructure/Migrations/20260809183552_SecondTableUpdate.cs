using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeldUntil",
                table: "ShowtimeSeat",
                newName: "ReservedUntil");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedAt",
                table: "ShowtimeSeat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReservedBy",
                table: "ShowtimeSeat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedAt",
                table: "ShowtimeSeat");

            migrationBuilder.DropColumn(
                name: "ReservedBy",
                table: "ShowtimeSeat");

            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "ReservedUntil",
                table: "ShowtimeSeat",
                newName: "HeldUntil");
        }
    }
}
