using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MrsCleanCapstone.Migrations
{
    public partial class AddedFeedbackDateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "Feedbacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "Feedbacks");
        }
    }
}
