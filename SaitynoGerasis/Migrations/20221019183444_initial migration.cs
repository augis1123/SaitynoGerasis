using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaitynoGerasis.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pardavejas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavadinimas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miestas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pardavejas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perkamapreke",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_PrekeId = table.Column<int>(type: "int", nullable: false),
                    fk_SaskaitaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perkamapreke", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Preke",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pavadinimas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aprasymas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kiekis = table.Column<int>(type: "int", nullable: false),
                    Kaina = table.Column<double>(type: "float", nullable: false),
                    fk_PardavejasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preke", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Saskaita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PirkimoData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miestas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saskaita", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pardavejas");

            migrationBuilder.DropTable(
                name: "perkamapreke");

            migrationBuilder.DropTable(
                name: "Preke");

            migrationBuilder.DropTable(
                name: "Saskaita");
        }
    }
}
