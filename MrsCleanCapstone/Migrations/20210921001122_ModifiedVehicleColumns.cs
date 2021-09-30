using Microsoft.EntityFrameworkCore.Migrations;

namespace MrsCleanCapstone.Migrations
{
    public partial class ModifiedVehicleColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Vehicles",
                newName: "ServiceType");

            migrationBuilder.RenameColumn(
                name: "Make",
                table: "Vehicles",
                newName: "Condition");

            migrationBuilder.AddColumn<int>(
                name: "NumSeats",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumSeats",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ServiceType",
                table: "Vehicles",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Vehicles",
                newName: "Make");
        }
    }
}
