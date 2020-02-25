using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieTransactionDetailRepository : IRepository<MovieTransactionDetail>
    {

        int CreateTransactionDetail(MovieTransactionDetail oDetail);
        decimal ReturnMovieRental(int iMovieTransactionDetailId);
    }
}
