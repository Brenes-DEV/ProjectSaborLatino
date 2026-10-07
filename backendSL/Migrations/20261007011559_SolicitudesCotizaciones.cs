using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectSaborLatino.Migrations
{
    /// <inheritdoc />
    public partial class SolicitudesCotizaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NombreContacto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CorreoContacto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PaqueteId = table.Column<int>(type: "int", nullable: true),
                    TipoEvento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CantidadInvitados = table.Column<int>(type: "int", nullable: true),
                    Comentarios = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_AspNetUsers_ClienteUsuarioId",
                        column: x => x.ClienteUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Paquetes_PaqueteId",
                        column: x => x.PaqueteId,
                        principalTable: "Paquetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    VigenteHasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreadaPorUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_AspNetUsers_CreadaPorUsuarioId",
                        column: x => x.CreadaPorUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_CreadaPorUsuarioId",
                table: "Cotizaciones",
                column: "CreadaPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_SolicitudId",
                table: "Cotizaciones",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ClienteUsuarioId",
                table: "Solicitudes",
                column: "ClienteUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_PaqueteId",
                table: "Solicitudes",
                column: "PaqueteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cotizaciones");

            migrationBuilder.DropTable(
                name: "Solicitudes");
        }
    }
}
