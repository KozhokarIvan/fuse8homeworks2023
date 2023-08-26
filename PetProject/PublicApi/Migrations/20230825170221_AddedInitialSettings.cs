using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "user",
                table: "settings",
                columns: new[] { "id", "name", "value" },
                values: new object[,]
                {
                    { 1, "defaultCurrency", "RUB" },
                    { 2, "currencyRoundCount", "2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "user",
                table: "settings",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "user",
                table: "settings",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
