using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenderPlatforms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(nullable: true),
                    TenderPlatformId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_TenderPlatforms_TenderPlatformId",
                        column: x => x.TenderPlatformId,
                        principalTable: "TenderPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenderFileAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenderId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ExtractedText = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true)
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
                name: "IX_Tenders_TenderPlatformId",
                table: "Tenders",
                column: "TenderPlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderFileAttachments");

            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropTable(
                name: "TenderPlatforms");
        }
    }
}
