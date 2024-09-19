using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingsApplication.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Artist", "Category", "DateTime", "Description", "Name", "TicketPrice", "Venue" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), "Dave Chappelle", "Comedy Show", new DateTime(2024, 11, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "A hilarious stand-up comedy night featuring top comedians.", "Laugh Riot", 49, "The Comedy Store, Los Angeles" },
                    { new Guid("a34b1f0a-7c5e-4df5-a312-fc90bbd6356d"), "London Shakespeare Company", "Theater", new DateTime(2024, 12, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), "A modern rendition of the classic play, Hamlet.", "Shakespeare's Hamlet", 120, "The Globe Theatre, London" },
                    { new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"), "The Rolling Stones", "Concert", new DateTime(2024, 10, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "An electrifying night with the biggest rock bands!", "Rock The Night", 99, "Madison Square Garden, New York" },
                    { new Guid("d81b2f5e-9c1e-4817-b6c5-bc3b146b2177"), "Herbie Hancock", "Concert", new DateTime(2024, 10, 20, 21, 0, 0, 0, DateTimeKind.Unspecified), "A smooth jazz evening with international jazz artists.", "Jazz Vibes", 79, "Blue Note, Tokyo" },
                    { new Guid("e4b1d89f-8272-4a2b-b8b8-2276f497912b"), "David Copperfield", "Magic Show", new DateTime(2024, 10, 31, 19, 0, 0, 0, DateTimeKind.Unspecified), "A night of mind-bending magic and illusions.", "Illusions Unveiled", 150, "Caesars Palace, Las Vegas" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
