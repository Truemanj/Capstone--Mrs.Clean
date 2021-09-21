using Microsoft.EntityFrameworkCore.Migrations;

namespace MrsCleanCapstone.Migrations
{
    public partial class ModifiedAppointmentColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Appointments");

            migrationBuilder.AddColumn<bool>(
                name: "anyPetHair",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "powerOutletAvailable",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "waterHoseAvailability",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "waterSupplyConnection",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anyPetHair",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "powerOutletAvailable",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "waterHoseAvailability",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "waterSupplyConnection",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
