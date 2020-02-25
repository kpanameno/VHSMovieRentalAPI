using Microsoft.EntityFrameworkCore.Migrations;

namespace VHSMovieRentalAPI.Migrations
{
    public partial class TransactionDetailPriceColType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MovieTransactionDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "MovieTransactionDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
