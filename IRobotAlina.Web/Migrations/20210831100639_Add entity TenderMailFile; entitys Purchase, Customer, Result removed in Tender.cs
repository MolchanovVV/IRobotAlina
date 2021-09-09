using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Web.Migrations
{
    public partial class AddentityTenderMailFileentitysPurchaseCustomerResultremovedinTender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tenders",
                type: "varchar(512)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenders",
                type: "nvarchar(2048)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tenders",
                type: "varchar(128)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_INN",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_KPP",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_PlaceDelivery",
                table: "Tenders",
                type: "nvarchar(1024)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Region",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Comment",
                table: "Tenders",
                type: "nvarchar(1024)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Purchase_ConductingSelection",
                table: "Tenders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Currency",
                table: "Tenders",
                type: "varchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Purchase_DeadlineAcceptApp",
                table: "Tenders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_ETP",
                table: "Tenders",
                type: "nvarchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_InitMinPrice",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Mark",
                table: "Tenders",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_Name",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Purchase_PublicationDate",
                table: "Tenders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_SecuringApp",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_SecuringContract",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_SelectionMethod",
                table: "Tenders",
                type: "varchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_SelectionStage",
                table: "Tenders",
                type: "varchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Purchase_TypeBidding",
                table: "Tenders",
                type: "varchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_ContractPrice",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_PercenteDecline",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Result_PublicationProtocol",
                table: "Tenders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_SupplierINN",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_SupplierName",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_WinnerINN",
                table: "Tenders",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_WinnerName",
                table: "Tenders",
                type: "nvarchar(512)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result_WinnerOffer",
                table: "Tenders",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tenders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TenderMails",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TenderFileAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "TenderFileAttachments",
                type: "nvarchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "TenderFileAttachments",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArchiveName",
                table: "TenderFileAttachments",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TenderMailFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialId()"),
                    TenderMailId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ParsedData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderMailFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenderMailFiles_TenderMails_TenderMailId",
                        column: x => x.TenderMailId,
                        principalTable: "TenderMails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenderMailFiles_TenderMailId",
                table: "TenderMailFiles",
                column: "TenderMailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenderMailFiles");

            migrationBuilder.DropColumn(
                name: "Customer_INN",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Customer_KPP",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Customer_PlaceDelivery",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Customer_Region",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_Comment",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_ConductingSelection",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_Currency",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_DeadlineAcceptApp",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_ETP",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_InitMinPrice",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_Mark",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_Name",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_PublicationDate",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_SecuringApp",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_SecuringContract",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_SelectionMethod",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_SelectionStage",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Purchase_TypeBidding",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_ContractPrice",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_PercenteDecline",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_PublicationProtocol",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_SupplierINN",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_SupplierName",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_WinnerINN",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_WinnerName",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Result_WinnerOffer",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tenders");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TenderMails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TenderFileAttachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "TenderFileAttachments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "TenderFileAttachments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ArchiveName",
                table: "TenderFileAttachments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);
        }
    }
}
