using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIResumePicker.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Criteria",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "CriteriaId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobCriteria",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Education = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Skills = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCriteria", x => x.CriteriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CriteriaId",
                table: "Jobs",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCriteria_CriteriaId",
                table: "Jobs",
                column: "CriteriaId",
                principalTable: "JobCriteria",
                principalColumn: "CriteriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCriteria_CriteriaId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobCriteria");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CriteriaId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Jobs",
                newName: "Criteria");
        }
    }
}
