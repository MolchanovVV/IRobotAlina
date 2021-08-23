using Microsoft.EntityFrameworkCore.Migrations;

namespace IRobotAlina.Data.Migrations
{
    public partial class ConfigurationItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationItems",
                columns: table => new
                {
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItems", x => x.Type);
                });

            migrationBuilder.InsertData(
                table: "ConfigurationItems",
                columns: new[] { "Type", "Value" },
                values: new object[,]
                {
                    { 0, null },
                    { 1, null },
                    { 2, null },
                    { 3, null },
                    { 4, null },
                    { 5, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItems");
        }
    }
}
