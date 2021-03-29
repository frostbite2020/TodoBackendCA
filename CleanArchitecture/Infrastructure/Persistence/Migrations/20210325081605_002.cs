using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoCategories_UserProps_UserPropertyId",
                table: "TodoCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_UserProps_UserPropertyId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_UserPropertyId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "UserPropertyId",
                table: "TodoItems");

            migrationBuilder.AlterColumn<int>(
                name: "UserPropertyId",
                table: "TodoCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoCategories_UserProps_UserPropertyId",
                table: "TodoCategories",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoCategories_UserProps_UserPropertyId",
                table: "TodoCategories");

            migrationBuilder.AddColumn<int>(
                name: "UserPropertyId",
                table: "TodoItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserPropertyId",
                table: "TodoCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_UserPropertyId",
                table: "TodoItems",
                column: "UserPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoCategories_UserProps_UserPropertyId",
                table: "TodoCategories",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_UserProps_UserPropertyId",
                table: "TodoItems",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
