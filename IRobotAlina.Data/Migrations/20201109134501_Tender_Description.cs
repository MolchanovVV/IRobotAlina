using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class Tender_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tenders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tenders");
        }
    }
}
