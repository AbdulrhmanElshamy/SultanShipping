using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SultanShipping.Migrations
{
    /// <inheritdoc />
    public partial class makeCancletionResoneRequierd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CancellationReason",
                table: "CustomerShipments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0191a4b6-c4fc-752e-9d95-40b30fa7a9b6",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBLM5XjYScnGXBG9Q6XOUGcSI1e/vD14KtkvODlCt9PLK+KFKs6ALhbhHdRckWok8g==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CancellationReason",
                table: "CustomerShipments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0191a4b6-c4fc-752e-9d95-40b30fa7a9b6",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELteWGrSkBxKvMfZIu4g0L9/wqaZytGiGf1GGagByS4dCTCd3xNi1V6ypTm0uW+YBQ==");
        }
    }
}
