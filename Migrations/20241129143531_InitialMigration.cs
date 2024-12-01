using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mindEaseAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "booking_information",
                columns: table => new
                {
                    bookingId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    clientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    appointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    appointmentSession = table.Column<DateTime>(type: "datetime2", nullable: false),
                    activeAppointment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_information", x => x.bookingId);
                });

            migrationBuilder.CreateTable(
                name: "client_information",
                columns: table => new
                {
                    clientId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    clientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientNumber = table.Column<int>(type: "int", nullable: false),
                    clientAge = table.Column<int>(type: "int", nullable: false),
                    clientGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clientPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reservedAppointment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_information", x => x.clientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "booking_information");

            migrationBuilder.DropTable(
                name: "client_information");
        }
    }
}
