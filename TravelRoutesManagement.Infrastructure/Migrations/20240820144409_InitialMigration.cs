using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelRoutesManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirportOriginId = table.Column<int>(type: "int", nullable: false),
                    AirportDestinationId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightConnections_Airports_AirportDestinationId",
                        column: x => x.AirportDestinationId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightConnections_Airports_AirportOriginId",
                        column: x => x.AirportOriginId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightConnections_AirportDestinationId",
                table: "FlightConnections",
                column: "AirportDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightConnections_AirportOriginId",
                table: "FlightConnections",
                column: "AirportOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightConnections_AirportOriginId_AirportDestinationId",
                table: "FlightConnections",
                columns: new[] { "AirportOriginId", "AirportDestinationId" },
                unique: true);

            // Carga de dados
            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "Id", "Name", "Acronym" },
                values: new object[,]
                {
                    { 1, "Aeroporto Internacional de Guarulhos – Cumbica", "GRU" },
                    { 2, "Aeroporto Internacional Teniente Luis Candelaria", "BRC" },
                    { 3, "Aeroporto Internacional de Orlando", "ORL" },
                    { 4, "Aeroporto Internacional Arturo Merino Benítez", "SCL" },
                    { 5, "Aeroporto de Paris-Charles de Gaulle", "CDG" }
                });

            migrationBuilder.InsertData(
                table: "FlightConnections",
                columns: new[] { "Id", "AirportOriginId", "AirportDestinationId", "Price" },
                values: new object[,]
                {
                    { 1, 1, 2, 10 },
                    { 2, 2, 4, 5 },
                    { 3, 1, 5, 75 },
                    { 4, 1, 4, 20 },
                    { 5, 1, 3, 56 },
                    { 6, 3, 5, 5 },
                    { 7, 4, 3, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightConnections");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
