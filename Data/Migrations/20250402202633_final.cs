using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca_MVC.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_Rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LimitePrestamo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Rol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_TipoMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_TipoMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Persona_Tb_Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Tb_Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_MaterialAcademico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdTipoMaterial = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidaRegistrada = table.Column<int>(type: "int", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_MaterialAcademico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_MaterialAcademico_Tb_TipoMaterial_IdTipoMaterial",
                        column: x => x.IdTipoMaterial,
                        principalTable: "Tb_TipoMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tb_Historial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMaterial = table.Column<int>(type: "int", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Historial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tb_Historial_Tb_MaterialAcademico_IdMaterial",
                        column: x => x.IdMaterial,
                        principalTable: "Tb_MaterialAcademico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tb_Historial_Tb_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Tb_Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Historial_IdMaterial",
                table: "Tb_Historial",
                column: "IdMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Historial_IdPersona",
                table: "Tb_Historial",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_MaterialAcademico_IdTipoMaterial",
                table: "Tb_MaterialAcademico",
                column: "IdTipoMaterial");

            migrationBuilder.CreateIndex(
                name: "IX_Tb_Persona_IdRol",
                table: "Tb_Persona",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_Historial");

            migrationBuilder.DropTable(
                name: "Tb_MaterialAcademico");

            migrationBuilder.DropTable(
                name: "Tb_Persona");

            migrationBuilder.DropTable(
                name: "Tb_TipoMaterial");

            migrationBuilder.DropTable(
                name: "Tb_Rol");
        }
    }
}
