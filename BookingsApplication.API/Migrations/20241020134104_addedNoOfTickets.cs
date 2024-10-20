using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingsApplication.API.Migrations
{
    /// <inheritdoc />
    public partial class addedNoOfTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("9d7cb4bb-a249-408d-a42b-5c6fceccae25"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("ba2c4982-b7bb-4196-b9d2-8631dd342500"));

            migrationBuilder.AddColumn<int>(
                name: "NoOfTickets",
                table: "TicketTypes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfTickets",
                table: "TicketTypes");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Artist", "AvailableTickets", "Category", "DateTime", "Description", "EventImage", "Name", "TicketPrice", "TotalTickets", "UserId", "Venue" },
                values: new object[,]
                {
                    { new Guid("9d7cb4bb-a249-408d-a42b-5c6fceccae25"), "Various Teams", 130000, "[\"Sports\",\"Cricket\"]", new DateTime(2025, 6, 19, 21, 46, 43, 701, DateTimeKind.Local).AddTicks(4986), "The grand finale of Indian Premier League 2025.", "[\"https://images.unsplash.com/photo-1540747913346-19e32dc3e97e\",\"https://images.unsplash.com/photo-1531415074968-036ba1b575da\"]", "IPL Final 2025", 2000, 130000, null, "Narendra Modi Stadium, Ahmedabad" },
                    { new Guid("ba2c4982-b7bb-4196-b9d2-8631dd342500"), "Arijit Singh", 10000, "[\"Music\",\"Concert\"]", new DateTime(2024, 11, 19, 21, 46, 43, 695, DateTimeKind.Local).AddTicks(4061), "A mesmerizing night of Bollywood music featuring Arijit Singh.", "[\"https://images.unsplash.com/photo-1533229505515-cbc2deef1f53\",\"https://images.unsplash.com/photo-1494790108377-be9c29b29330\"]", "Bollywood Beats 2024", 1000, 10000, null, "Wankhede Stadium, Mumbai" }
                });
        }
    }
}
