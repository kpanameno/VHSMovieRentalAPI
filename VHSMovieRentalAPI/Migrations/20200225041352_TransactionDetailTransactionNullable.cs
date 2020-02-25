using Microsoft.EntityFrameworkCore.Migrations;

namespace VHSMovieRentalAPI.Migrations
{
    public partial class TransactionDetailTransactionNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTransactionDetail_MovieRentalTerm_MovieRentalTermID",
                table: "MovieTransactionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "MovieRentalTermID",
                table: "MovieTransactionDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTransactionDetail_MovieRentalTerm_MovieRentalTermID",
                table: "MovieTransactionDetail",
                column: "MovieRentalTermID",
                principalTable: "MovieRentalTerm",
                principalColumn: "MovieRentalTermID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTransactionDetail_MovieRentalTerm_MovieRentalTermID",
                table: "MovieTransactionDetail");

            migrationBuilder.AlterColumn<int>(
                name: "MovieRentalTermID",
                table: "MovieTransactionDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTransactionDetail_MovieRentalTerm_MovieRentalTermID",
                table: "MovieTransactionDetail",
                column: "MovieRentalTermID",
                principalTable: "MovieRentalTerm",
                principalColumn: "MovieRentalTermID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
