using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class TenderMail_Content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "TenderMails");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "TenderMails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "TenderMails");

            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "TenderMails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
