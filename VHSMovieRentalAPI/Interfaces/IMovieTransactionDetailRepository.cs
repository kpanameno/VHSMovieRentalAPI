using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieTransactionDetailRepository : IRepository<MovieTransactionDetail>
    {

        int ValidateTransactionDetail(int iTransactionUserID,  MovieTransactionDetail oDetail, out string sErrorMessage);
        decimal ReturnMovieRental(int iMovieTransactionDetailId);
    }
}
