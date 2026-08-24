using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorpExpenseApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureExpenseItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpenseId",
                table: "ExpenseItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseItem",
                table: "ExpenseItem");

            migrationBuilder.RenameTable(
                name: "ExpenseItem",
                newName: "ExpenseItems");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseItem_ExpenseId",
                table: "ExpenseItems",
                newName: "IX_ExpenseItems_ExpenseId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ExpenseItems",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ExpenseItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseItems",
                table: "ExpenseItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItems_Expenses_ExpenseId",
                table: "ExpenseItems",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItems_Expenses_ExpenseId",
                table: "ExpenseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseItems",
                table: "ExpenseItems");

            migrationBuilder.RenameTable(
                name: "ExpenseItems",
                newName: "ExpenseItem");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseItems_ExpenseId",
                table: "ExpenseItem",
                newName: "IX_ExpenseItem_ExpenseId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ExpenseItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ExpenseItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseItem",
                table: "ExpenseItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpenseId",
                table: "ExpenseItem",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
