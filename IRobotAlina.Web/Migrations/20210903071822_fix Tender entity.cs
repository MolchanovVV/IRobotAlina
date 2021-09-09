using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Web.Migrations
{
    public partial class fixTenderentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purchase_Name",
                table: "Tenders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Purchase_Name",
                table: "Tenders",
                type: "nvarchar(2048)",
                nullable: true);
        }
    }
}
