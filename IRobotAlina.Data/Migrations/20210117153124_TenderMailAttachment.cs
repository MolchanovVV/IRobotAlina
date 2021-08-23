using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class TenderMailAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderFileAttachments_Tenders_TenderId",
                table: "TenderFileAttachments");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                table: "TenderFileAttachments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "TenderMailAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailId = table.Column<int>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ExtractedText = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    ExceptionMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderMailAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderMailAttachment_TenderMails_MailId",
                        column: x => x.MailId,
                        principalTable: "TenderMails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenderMailAttachment_MailId",
                table: "TenderMailAttachment",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenderFileAttachments_Tenders_TenderId",
                table: "TenderFileAttachments",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenderFileAttachments_Tenders_TenderId",
                table: "TenderFileAttachments");

            migrationBuilder.DropTable(
                name: "TenderMailAttachment");

            migrationBuilder.AlterColumn<int>(
                name: "TenderId",
                table: "TenderFileAttachments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TenderFileAttachments_Tenders_TenderId",
                table: "TenderFileAttachments",
                column: "TenderId",
                principalTable: "Tenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
