using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cur");

            migrationBuilder.CreateTable(
                name: "currency_codes",
                schema: "cur",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "cur",
                columns: table => new
                {
                    currency_code_id = table.Column<int>(type: "integer", nullable: false),
                    base_currency_code_id = table.Column<int>(type: "integer", nullable: false),
                    date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => new { x.date_time, x.base_currency_code_id, x.currency_code_id });
                    table.ForeignKey(
                        name: "fk_currencies_currency_codes_base_currency_code_id",
                        column: x => x.base_currency_code_id,
                        principalSchema: "cur",
                        principalTable: "currency_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_currencies_currency_codes_currency_code_id",
                        column: x => x.currency_code_id,
                        principalSchema: "cur",
                        principalTable: "currency_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_currencies_base_currency_code_id",
                schema: "cur",
                table: "currencies",
                column: "base_currency_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_currencies_currency_code_id",
                schema: "cur",
                table: "currencies",
                column: "currency_code_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currencies",
                schema: "cur");

            migrationBuilder.DropTable(
                name: "currency_codes",
                schema: "cur");
        }
    }
}
