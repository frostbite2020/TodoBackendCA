using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories");

            migrationBuilder.AlterColumn<int>(
                name: "TodoDailyId",
                table: "TodoDailyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TodoDailyActivity",
                table: "TodoDailyHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories",
                column: "TodoDailyId",
                principalTable: "TodoDailys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories");

            migrationBuilder.DropColumn(
                name: "TodoDailyActivity",
                table: "TodoDailyHistories");

            migrationBuilder.AlterColumn<int>(
                name: "TodoDailyId",
                table: "TodoDailyHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                table: "TodoDailyHistories",
                column: "TodoDailyId",
                principalTable: "TodoDailys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
