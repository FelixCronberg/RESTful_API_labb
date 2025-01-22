using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class RemakingCauseOutOfSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true),
                    ReleaseYear = table.Column<int>(type: "int", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.CheckConstraint("CK_Book_Rating", "[Rating] > 0 AND [Rating] < 10");
                    table.CheckConstraint("CK_Book_ReleaseYear", "[ReleaseYear] > 0 AND [ReleaseYear] < 2500");
                });

            migrationBuilder.CreateTable(
                name: "LoanCardOwner",
                columns: table => new
                {
                    LoanCardOwnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanCardOwner", x => x.LoanCardOwnerId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanCard",
                columns: table => new
                {
                    LoanCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "DATEADD(YEAR, 5, GETDATE())"),
                    LoanCardOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanCard", x => x.LoanCardId);
                    table.ForeignKey(
                        name: "FK_LoanCard_LoanCardOwner_LoanCardOwnerId",
                        column: x => x.LoanCardOwnerId,
                        principalTable: "LoanCardOwner",
                        principalColumn: "LoanCardOwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()"),
                    ReturnedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    LoanCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loan_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_LoanCard_LoanCardId",
                        column: x => x.LoanCardId,
                        principalTable: "LoanCard",
                        principalColumn: "LoanCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BookId",
                table: "Loan",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanCardId",
                table: "Loan",
                column: "LoanCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanCard_LoanCardOwnerId",
                table: "LoanCard",
                column: "LoanCardOwnerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "LoanCard");

            migrationBuilder.DropTable(
                name: "LoanCardOwner");
        }
    }
}
