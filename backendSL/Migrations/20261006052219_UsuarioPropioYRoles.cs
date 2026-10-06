using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectSaborLatino.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioPropioYRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");
        }
    }
}
