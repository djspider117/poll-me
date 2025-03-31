using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollMe.Nyx.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSetEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Used = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataSetId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSetEntries_DataSets_DataSetId",
                        column: x => x.DataSetId,
                        principalTable: "DataSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSetEntries_DataSetId",
                table: "DataSetEntries",
                column: "DataSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSetEntries");

            migrationBuilder.DropTable(
                name: "DataSets");
        }
    }
}
