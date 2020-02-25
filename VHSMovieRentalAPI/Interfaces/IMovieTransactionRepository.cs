using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieTransactionRepository : IRepository<MovieTransaction>
    {

        MovieTransaction GetWithDetailsById(int iTransactionID);
        int CreateTransaction(int iUserTransactionID, MovieTransaction oMovieTransaction, out string sErrorMessage);
    }
}
