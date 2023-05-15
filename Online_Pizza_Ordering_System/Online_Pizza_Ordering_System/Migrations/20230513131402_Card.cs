using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Pizza_Ordering_System.Migrations
{
    /// <inheritdoc />
    public partial class Card : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "CardPayment");

            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                table: "CardPayment",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "CardPayment",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CardPayment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CardPayment_UserId",
                table: "CardPayment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardPayment_AspNetUsers_UserId",
                table: "CardPayment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardPayment_AspNetUsers_UserId",
                table: "CardPayment");

            migrationBuilder.DropIndex(
                name: "IX_CardPayment_UserId",
                table: "CardPayment");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "CardPayment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CardPayment");

            migrationBuilder.AlterColumn<int>(
                name: "OrderTotal",
                table: "CardPayment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "CardPayment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
