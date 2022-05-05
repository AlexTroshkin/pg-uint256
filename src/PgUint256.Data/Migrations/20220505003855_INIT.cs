using System.Globalization;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PgUint256.Data.Migrations
{
    public partial class INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Address = table.Column<string>(type: "character varying(42)", maxLength: 42, nullable: false),
                    Amount = table.Column<BigInteger>(type: "numeric(156,0)", precision: 156, scale: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Address", "Amount" },
                values: new object[,]
                {
                    { 1L, "0xc4e7b02446f87386faf9b99e6d2f828f64c3fee7", BigInteger.Parse("115792089237316195423570985008687907853269984665640564039457584007913129639935", NumberFormatInfo.InvariantInfo) },
                    { 2L, "0xFbdDaDD80fe7bda00B901FbAf73803F2238Ae655", BigInteger.Parse("6116130967035491411282519902102901479169861", NumberFormatInfo.InvariantInfo) },
                    { 3L, "0x76e6e2E9Ca0ee9a2BB4148566791FD6F2fEeAc32", BigInteger.Parse("77600947487271298400590085802719009615471363693269818666652196571960497734", NumberFormatInfo.InvariantInfo) },
                    { 4L, "0x95aD61b0a150d79219dCF64E1E6Cc01f0B64C4cE", BigInteger.Parse("644635553935915926370", NumberFormatInfo.InvariantInfo) },
                    { 5L, "0xF33010d57c8ef2d1F8BfDd8B1f4bdB95325f2478", BigInteger.Parse("0", NumberFormatInfo.InvariantInfo) },
                    { 6L, "0x1D0EBe30B558c4B7752c8C47166123C8a924bCdf", BigInteger.Parse("90825904533199959", NumberFormatInfo.InvariantInfo) },
                    { 7L, "0xeE8B2bd9B9F51C6584c751172A0294f652FD790b", BigInteger.Parse("898561960255311071189251597992482498246314843902477377209272966919", NumberFormatInfo.InvariantInfo) },
                    { 8L, "0xEBd9D99A3982d547C5Bb4DB7E3b1F9F14b67Eb83", BigInteger.Parse("32799005422311601657892591803566040868654766679566", NumberFormatInfo.InvariantInfo) },
                    { 9L, "0x3D80b8A0929070038B913BC054aEd56961307145", BigInteger.Parse("199969812050627495071995560782982", NumberFormatInfo.InvariantInfo) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
