using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingsApplication.API.Migrations
{
    /// <inheritdoc />
    public partial class seededAgainData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Artist", "AvailableTickets", "Category", "DateTime", "Description", "EventImage", "Name", "TicketPrice", "TotalTickets", "UserId", "Venue" },
                values: new object[,]
                {
                    { new Guid("19822555-e0f1-4710-ad64-5e05fe658e88"), "Various Teams", 130000, "[\"Sports\",\"Cricket\"]", new DateTime(2025, 6, 20, 20, 31, 51, 549, DateTimeKind.Local).AddTicks(7005), "The grand finale of Indian Premier League 2025.", "[\"https://images.unsplash.com/photo-1540747913346-19e32dc3e97e\",\"https://images.unsplash.com/photo-1531415074968-036ba1b575da\"]", "IPL Final 2025", 2000, 130000, null, "Narendra Modi Stadium, Ahmedabad" },
                    { new Guid("c861ee0b-7e5b-41e1-9983-188be3927a42"), "Arijit Singh", 10000, "[\"Music\",\"Concert\"]", new DateTime(2024, 11, 20, 20, 31, 51, 548, DateTimeKind.Local).AddTicks(6390), "A mesmerizing night of Bollywood music featuring Arijit Singh.", "[\"https://images.unsplash.com/photo-1533229505515-cbc2deef1f53\",\"https://images.unsplash.com/photo-1494790108377-be9c29b29330\"]", "Bollywood Beats 2024", 1000, 10000, null, "Wankhede Stadium, Mumbai" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("19822555-e0f1-4710-ad64-5e05fe658e88"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("c861ee0b-7e5b-41e1-9983-188be3927a42"));
        }
    }
}
