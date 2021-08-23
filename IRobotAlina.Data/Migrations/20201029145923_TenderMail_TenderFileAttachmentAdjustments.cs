using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class TenderMail_TenderFileAttachmentAdjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailId",
                table: "Tenders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchiveName",
                table: "TenderFileAttachments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_MailId",
                table: "Tenders",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenders_TenderMails_MailId",
                table: "Tenders",
                column: "MailId",
                principalTable: "TenderMails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenders_TenderMails_MailId",
                table: "Tenders");

            migrationBuilder.DropIndex(
                name: "IX_Tenders_MailId",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "MailId",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "ArchiveName",
                table: "TenderFileAttachments");
        }
    }
}
