using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingAdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancellationPolicy",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyRate",
                table: "Bookings",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceType",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleCategory",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "VehicleIsAvailable",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancellationPolicy",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleCategory",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "VehicleIsAvailable",
                table: "Bookings");
        }
    }
}
