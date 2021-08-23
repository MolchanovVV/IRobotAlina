using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Web.Migrations
{
    public partial class updateschemaDBTenderPlatformfromTenderstoTenderMails : Migration
    {

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "TenderFileAttachments");

            //migrationBuilder.DropTable(
            //    name: "Tenders");

            //migrationBuilder.DropTable(
            //    name: "TenderMails");

            //migrationBuilder.DropTable(
            //    name: "TenderPlatforms");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenderPlatforms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    TenderPlatformId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    UIDL = table.Column<string>(type: "varchar(200)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HTMLBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerTextBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderMails_TenderPlatforms_TenderPlatformId",
                        column: x => x.TenderPlatformId,
                        principalTable: "TenderPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    TenderMailId = table.Column<int>(type: "int", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_TenderMails_TenderMailId",
                        column: x => x.TenderMailId,
                        principalTable: "TenderMails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenderFileAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenderId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchiveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtractedText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsArchive = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderFileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderFileAttachments_Tenders_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
           
            migrationBuilder.InsertData(
                table: "TenderPlatforms",
                columns: new[] { "Id", "Name" },
                values: new object[] { "58fbc48e-b203-42b0-8d08-caf75a1a4ed1", "Zakupki" });

            migrationBuilder.CreateIndex(
                name: "IX_TenderFileAttachments_TenderId",
                table: "TenderFileAttachments",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderMails_TenderPlatformId",
                table: "TenderMails",
                column: "TenderPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_TenderMailId",
                table: "Tenders",
                column: "TenderMailId");
        }        
    }
}
