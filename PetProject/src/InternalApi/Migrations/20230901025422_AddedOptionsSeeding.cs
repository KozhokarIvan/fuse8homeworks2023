using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedOptionsSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "cur",
                table: "settings",
                columns: new[] { "id", "name", "value" },
                values: new object[] { 1, "baseCurrency", "USD" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "cur",
                table: "settings",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
