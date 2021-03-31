using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPropertyId",
                table: "TodoDailyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TodoDailyHistories_UserPropertyId",
                table: "TodoDailyHistories",
                column: "UserPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailyHistories_UserProps_UserPropertyId",
                table: "TodoDailyHistories",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailyHistories_UserProps_UserPropertyId",
                table: "TodoDailyHistories");

            migrationBuilder.DropIndex(
                name: "IX_TodoDailyHistories_UserPropertyId",
                table: "TodoDailyHistories");

            migrationBuilder.DropColumn(
                name: "UserPropertyId",
                table: "TodoDailyHistories");
        }
    }
}
