using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCacheTaskEntitiesSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "cur",
                table: "cache_task_statuses",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Задача создана" },
                    { 2, "Задача в обработке" },
                    { 3, "Задача завершена успешно" },
                    { 4, "Задача завершена с ошибкой" },
                    { 5, "Задача отменена" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "cur",
                table: "cache_task_statuses",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "cache_task_statuses",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "cache_task_statuses",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "cache_task_statuses",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "cache_task_statuses",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
