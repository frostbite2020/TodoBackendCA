using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories");

            migrationBuilder.DropIndex(
                name: "IX_TodoDailyHistories_TodoDailyId",
                table: "TodoDailyHistories");

            migrationBuilder.DropColumn(
                name: "TodoDailyId",
                table: "TodoDailyHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoDailyId",
                table: "TodoDailyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TodoDailyHistories_TodoDailyId",
                table: "TodoDailyHistories",
                column: "TodoDailyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories",
                column: "TodoDailyId",
                principalTable: "TodoDailys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
