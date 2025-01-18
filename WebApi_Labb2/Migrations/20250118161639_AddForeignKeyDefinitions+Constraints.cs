using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyDefinitionsConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Author_AuthorsAuthorId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BooksBookId",
                table: "AuthorBook");

            migrationBuilder.DropIndex(
                name: "IX_LoanCard_LoanCardOwnerId",
                table: "LoanCard");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BooksBookId",
                table: "AuthorBook",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "AuthorsAuthorId",
                table: "AuthorBook",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BooksBookId",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BookId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "LoanCardOwner",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "LoanCardOwner",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "IssueDate",
                table: "LoanCard",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpirationDate",
                table: "LoanCard",
                type: "date",
                nullable: false,
                defaultValueSql: "DATEADD(YEAR, 5, GETDATE())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LoanedDate",
                table: "Loan",
                type: "smalldatetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedDate",
                table: "Loan",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Author",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Author",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LoanCard_LoanCardOwnerId",
                table: "LoanCard",
                column: "LoanCardOwnerId",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Book_Rating",
                table: "Books",
                sql: "[Rating] > 0 AND [Rating] < 10");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Book_ReleaseYear",
                table: "Books",
                sql: "[ReleaseYear] > 0 AND [ReleaseYear] < 2500");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Author_AuthorId",
                table: "AuthorBook",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BookId",
                table: "AuthorBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Author_AuthorId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BookId",
                table: "AuthorBook");

            migrationBuilder.DropIndex(
                name: "IX_LoanCard_LoanCardOwnerId",
                table: "LoanCard");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Book_Rating",
                table: "Books");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Book_ReleaseYear",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReturnedDate",
                table: "Loan");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "AuthorBook",
                newName: "BooksBookId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AuthorBook",
                newName: "AuthorsAuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BooksBookId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "LoanCardOwner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "LoanCardOwner",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IssueDate",
                table: "LoanCard",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "LoanCard",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "DATEADD(YEAR, 5, GETDATE())");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "LoanedDate",
                table: "Loan",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReturnDate",
                table: "Loan",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Author",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.CreateIndex(
                name: "IX_LoanCard_LoanCardOwnerId",
                table: "LoanCard",
                column: "LoanCardOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Author_AuthorsAuthorId",
                table: "AuthorBook",
                column: "AuthorsAuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BooksBookId",
                table: "AuthorBook",
                column: "BooksBookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
