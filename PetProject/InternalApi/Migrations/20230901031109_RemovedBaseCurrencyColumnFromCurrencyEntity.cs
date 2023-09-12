using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBaseCurrencyColumnFromCurrencyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_currencies_currency_codes_base_currency_code_id",
                schema: "cur",
                table: "currencies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currencies",
                schema: "cur",
                table: "currencies");

            migrationBuilder.DropIndex(
                name: "ix_currencies_base_currency_code_id",
                schema: "cur",
                table: "currencies");

            migrationBuilder.DropColumn(
                name: "base_currency_code_id",
                schema: "cur",
                table: "currencies");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currencies",
                schema: "cur",
                table: "currencies",
                columns: new[] { "date_time", "currency_code_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_currencies",
                schema: "cur",
                table: "currencies");

            migrationBuilder.AddColumn<int>(
                name: "base_currency_code_id",
                schema: "cur",
                table: "currencies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "pk_currencies",
                schema: "cur",
                table: "currencies",
                columns: new[] { "date_time", "base_currency_code_id", "currency_code_id" });

            migrationBuilder.CreateIndex(
                name: "ix_currencies_base_currency_code_id",
                schema: "cur",
                table: "currencies",
                column: "base_currency_code_id");

            migrationBuilder.AddForeignKey(
                name: "fk_currencies_currency_codes_base_currency_code_id",
                schema: "cur",
                table: "currencies",
                column: "base_currency_code_id",
                principalSchema: "cur",
                principalTable: "currency_codes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
