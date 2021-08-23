using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class TenderMail_Body_Attachment_ExceptionMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HTMLBody",
                table: "TenderMails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InnerTextBody",
                table: "TenderMails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionMessage",
                table: "TenderFileAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HTMLBody",
                table: "TenderMails");

            migrationBuilder.DropColumn(
                name: "InnerTextBody",
                table: "TenderMails");

            migrationBuilder.DropColumn(
                name: "ExceptionMessage",
                table: "TenderFileAttachments");
        }
    }
}
