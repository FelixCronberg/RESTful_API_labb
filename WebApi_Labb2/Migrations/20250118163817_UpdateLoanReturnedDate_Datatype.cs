using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoanReturnedDate_Datatype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnedDate",
                table: "Loan",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnedDate",
                table: "Loan",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);
        }
    }
}
