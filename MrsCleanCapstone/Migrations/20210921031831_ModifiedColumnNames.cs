using Microsoft.EntityFrameworkCore.Migrations;

namespace MrsCleanCapstone.Migrations
{
    public partial class ModifiedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "waterSupplyConnection",
                table: "Appointments",
                newName: "WaterSupplyConnection");

            migrationBuilder.RenameColumn(
                name: "waterHoseAvailability",
                table: "Appointments",
                newName: "WaterHoseAvailability");

            migrationBuilder.RenameColumn(
                name: "powerOutletAvailable",
                table: "Appointments",
                newName: "PowerOutletAvailable");

            migrationBuilder.RenameColumn(
                name: "anyPetHair",
                table: "Appointments",
                newName: "AnyPetHair");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "WaterSupplyConnection",
                table: "Appointments",
                newName: "waterSupplyConnection");

            migrationBuilder.RenameColumn(
                name: "WaterHoseAvailability",
                table: "Appointments",
                newName: "waterHoseAvailability");

            migrationBuilder.RenameColumn(
                name: "PowerOutletAvailable",
                table: "Appointments",
                newName: "powerOutletAvailable");

            migrationBuilder.RenameColumn(
                name: "AnyPetHair",
                table: "Appointments",
                newName: "anyPetHair");
        }
    }
}
