using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedInitialCurrencyCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "cur",
                table: "currency_codes",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "ada" },
                    { 2, "aed" },
                    { 3, "afn" },
                    { 4, "all" },
                    { 5, "amd" },
                    { 6, "ang" },
                    { 7, "aoa" },
                    { 8, "arb" },
                    { 9, "ars" },
                    { 10, "aud" },
                    { 11, "avax" },
                    { 12, "awg" },
                    { 13, "azn" },
                    { 14, "bam" },
                    { 15, "bbd" },
                    { 16, "bdt" },
                    { 17, "bgn" },
                    { 18, "bhd" },
                    { 19, "bif" },
                    { 20, "bmd" },
                    { 21, "bnb" },
                    { 22, "bnd" },
                    { 23, "bob" },
                    { 24, "brl" },
                    { 25, "bsd" },
                    { 26, "btc" },
                    { 27, "btn" },
                    { 28, "busd" },
                    { 29, "bwp" },
                    { 30, "byn" },
                    { 31, "byr" },
                    { 32, "bzd" },
                    { 33, "cad" },
                    { 34, "cdf" },
                    { 35, "chf" },
                    { 36, "clf" },
                    { 37, "clp" },
                    { 38, "cny" },
                    { 39, "cop" },
                    { 40, "crc" },
                    { 41, "cuc" },
                    { 42, "cup" },
                    { 43, "cve" },
                    { 44, "czk" },
                    { 45, "dai" },
                    { 46, "djf" },
                    { 47, "dkk" },
                    { 48, "dop" },
                    { 49, "dot" },
                    { 50, "dzd" },
                    { 51, "egp" },
                    { 52, "ern" },
                    { 53, "etb" },
                    { 54, "eth" },
                    { 55, "eur" },
                    { 56, "fjd" },
                    { 57, "fkp" },
                    { 58, "gbp" },
                    { 59, "gel" },
                    { 60, "ggp" },
                    { 61, "ghs" },
                    { 62, "gip" },
                    { 63, "gmd" },
                    { 64, "gnf" },
                    { 65, "gtq" },
                    { 66, "gyd" },
                    { 67, "hkd" },
                    { 68, "hnl" },
                    { 69, "hrk" },
                    { 70, "htg" },
                    { 71, "huf" },
                    { 72, "idr" },
                    { 73, "ils" },
                    { 74, "imp" },
                    { 75, "inr" },
                    { 76, "iqd" },
                    { 77, "irr" },
                    { 78, "isk" },
                    { 79, "jep" },
                    { 80, "jmd" },
                    { 81, "jod" },
                    { 82, "jpy" },
                    { 83, "kes" },
                    { 84, "kgs" },
                    { 85, "khr" },
                    { 86, "kmf" },
                    { 87, "kpw" },
                    { 88, "krw" },
                    { 89, "kwd" },
                    { 90, "kyd" },
                    { 91, "kzt" },
                    { 92, "lak" },
                    { 93, "lbp" },
                    { 94, "lkr" },
                    { 95, "lrd" },
                    { 96, "lsl" },
                    { 97, "ltc" },
                    { 98, "ltl" },
                    { 99, "lvl" },
                    { 100, "lyd" },
                    { 101, "mad" },
                    { 102, "matic" },
                    { 103, "mdl" },
                    { 104, "mga" },
                    { 105, "mkd" },
                    { 106, "mmk" },
                    { 107, "mnt" },
                    { 108, "mop" },
                    { 109, "mro" },
                    { 110, "mur" },
                    { 111, "mvr" },
                    { 112, "mwk" },
                    { 113, "mxn" },
                    { 114, "myr" },
                    { 115, "mzn" },
                    { 116, "nad" },
                    { 117, "ngn" },
                    { 118, "nio" },
                    { 119, "nok" },
                    { 120, "npr" },
                    { 121, "nzd" },
                    { 122, "omr" },
                    { 123, "op" },
                    { 124, "pab" },
                    { 125, "pen" },
                    { 126, "pgk" },
                    { 127, "php" },
                    { 128, "pkr" },
                    { 129, "pln" },
                    { 130, "pyg" },
                    { 131, "qar" },
                    { 132, "ron" },
                    { 133, "rsd" },
                    { 134, "rub" },
                    { 135, "rwf" },
                    { 136, "sar" },
                    { 137, "sbd" },
                    { 138, "scr" },
                    { 139, "sdg" },
                    { 140, "sek" },
                    { 141, "sgd" },
                    { 142, "shp" },
                    { 143, "sll" },
                    { 144, "sol" },
                    { 145, "sos" },
                    { 146, "srd" },
                    { 147, "std" },
                    { 148, "svc" },
                    { 149, "syp" },
                    { 150, "szl" },
                    { 151, "thb" },
                    { 152, "tjs" },
                    { 153, "tmt" },
                    { 154, "tnd" },
                    { 155, "top" },
                    { 156, "try" },
                    { 157, "ttd" },
                    { 158, "twd" },
                    { 159, "tzs" },
                    { 160, "uah" },
                    { 161, "ugx" },
                    { 162, "usd" },
                    { 163, "usdc" },
                    { 164, "usdt" },
                    { 165, "uyu" },
                    { 166, "uzs" },
                    { 167, "vef" },
                    { 168, "vnd" },
                    { 169, "vuv" },
                    { 170, "wst" },
                    { 171, "xaf" },
                    { 172, "xag" },
                    { 173, "xau" },
                    { 174, "xcd" },
                    { 175, "xdr" },
                    { 176, "xof" },
                    { 177, "xpd" },
                    { 178, "xpf" },
                    { 179, "xpt" },
                    { 180, "xrp" },
                    { 181, "yer" },
                    { 182, "zar" },
                    { 183, "zmk" },
                    { 184, "zmw" },
                    { 185, "zwl" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                schema: "cur",
                table: "currency_codes",
                keyColumn: "id",
                keyValue: 185);
        }
    }
}
