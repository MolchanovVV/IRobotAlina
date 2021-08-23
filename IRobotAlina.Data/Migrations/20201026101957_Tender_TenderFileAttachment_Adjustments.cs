using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class Tender_TenderFileAttachment_Adjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tenders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "TenderFileAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "TenderFileAttachments");
        }
    }
}
