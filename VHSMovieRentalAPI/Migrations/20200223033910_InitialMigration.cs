using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VHSMovieRentalAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieRentalTerm",
                columns: table => new
                {
                    MovieRentalTermID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalDays = table.Column<int>(nullable: false),
                    LateReturnCharge = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRentalTerm", x => x.MovieRentalTermID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    TransactionTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoviePriceLogs",
                columns: table => new
                {
                    MoviePriceLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    RentalPrice = table.Column<decimal>(nullable: false),
                    SalePrice = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UpdatedUserID = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePriceLogs", x => x.MoviePriceLogID);
                    table.ForeignKey(
                        name: "FK_MoviePriceLogs_Users_UpdatedUserID",
                        column: x => x.UpdatedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    RentalPrice = table.Column<decimal>(nullable: false),
                    SalePrice = table.Column<decimal>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UpdatedUserID = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                    table.ForeignKey(
                        name: "FK_Movies_Users_UpdatedUserID",
                        column: x => x.UpdatedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieTransaction",
                columns: table => new
                {
                    MovieTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionUserID = table.Column<int>(nullable: false),
                    PaymentType = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    UpdatedUserID = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTransaction", x => x.MovieTransactionID);
                    table.ForeignKey(
                        name: "FK_MovieTransaction_Users_UpdatedUserID",
                        column: x => x.UpdatedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieTransaction_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieLikes",
                columns: table => new
                {
                    MovieLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieLikes", x => x.MovieLikeID);
                    table.ForeignKey(
                        name: "FK_MovieLikes_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieLikes_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieTransactionDetail",
                columns: table => new
                {
                    MovieTransactionDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTransactionID = table.Column<int>(nullable: false),
                    MovieID = table.Column<int>(nullable: false),
                    TransactionTypeID = table.Column<int>(nullable: false),
                    Returned = table.Column<bool>(nullable: false),
                    MovieRentalTermID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UpdatedUserID = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTransactionDetail", x => x.MovieTransactionDetailID);
                    table.ForeignKey(
                        name: "FK_MovieTransactionDetail_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieTransactionDetail_MovieRentalTerm_MovieRentalTermID",
                        column: x => x.MovieRentalTermID,
                        principalTable: "MovieRentalTerm",
                        principalColumn: "MovieRentalTermID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieTransactionDetail_MovieTransaction_MovieTransactionID",
                        column: x => x.MovieTransactionID,
                        principalTable: "MovieTransaction",
                        principalColumn: "MovieTransactionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieTransactionDetail_TransactionType_TransactionTypeID",
                        column: x => x.TransactionTypeID,
                        principalTable: "TransactionType",
                        principalColumn: "TransactionTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieTransactionDetail_Users_UpdatedUserID",
                        column: x => x.UpdatedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieLikes_MovieID",
                table: "MovieLikes",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieLikes_UserID",
                table: "MovieLikes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePriceLogs_UpdatedUserID",
                table: "MoviePriceLogs",
                column: "UpdatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_UpdatedUserID",
                table: "Movies",
                column: "UpdatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransaction_UpdatedUserID",
                table: "MovieTransaction",
                column: "UpdatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransaction_UserID",
                table: "MovieTransaction",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransactionDetail_MovieID",
                table: "MovieTransactionDetail",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransactionDetail_MovieRentalTermID",
                table: "MovieTransactionDetail",
                column: "MovieRentalTermID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransactionDetail_MovieTransactionID",
                table: "MovieTransactionDetail",
                column: "MovieTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransactionDetail_TransactionTypeID",
                table: "MovieTransactionDetail",
                column: "TransactionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTransactionDetail_UpdatedUserID",
                table: "MovieTransactionDetail",
                column: "UpdatedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieLikes");

            migrationBuilder.DropTable(
                name: "MoviePriceLogs");

            migrationBuilder.DropTable(
                name: "MovieTransactionDetail");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "MovieRentalTerm");

            migrationBuilder.DropTable(
                name: "MovieTransaction");

            migrationBuilder.DropTable(
                name: "TransactionType");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
