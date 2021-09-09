using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Web.Migrations
{
    public partial class SixfieldNumberlenghtinTenderPurchaseentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Purchase_Name",
                table: "Tenders",
                type: "nvarchar(2048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Purchase_Name",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldNullable: true);
        }
    }
}
