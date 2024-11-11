using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedGarageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkingGarages_ParkingGarageId",
                table: "ParkingSpots");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingGarageId",
                table: "ParkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_ParkingGarages_ParkingGarageId",
                table: "ParkingSpots",
                column: "ParkingGarageId",
                principalTable: "ParkingGarages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpots_ParkingGarages_ParkingGarageId",
                table: "ParkingSpots");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingGarageId",
                table: "ParkingSpots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpots_ParkingGarages_ParkingGarageId",
                table: "ParkingSpots",
                column: "ParkingGarageId",
                principalTable: "ParkingGarages",
                principalColumn: "Id");
        }
    }
}
