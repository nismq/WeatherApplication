using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locality",
                columns: table => new
                {
                    localityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    localityName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__locality__EBA257B25F0CDFA4", x => x.localityId);
                });

            migrationBuilder.CreateTable(
                name: "weatherdata",
                columns: table => new
                {
                    weatherdataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    temperature = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    rainfall = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    wind_speed = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    localityId = table.Column<int>(type: "int", nullable: true),
                    day = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__weatherd__49831657EAC52DD9", x => x.weatherdataId);
                    table.ForeignKey(
                        name: "FK__weatherda__local__2B3F6F97",
                        column: x => x.localityId,
                        principalTable: "locality",
                        principalColumn: "localityId");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__locality__71FF7E86D93DE688",
                table: "locality",
                column: "localityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_weatherdata_localityId",
                table: "weatherdata",
                column: "localityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weatherdata");

            migrationBuilder.DropTable(
                name: "locality");
        }
    }
}
