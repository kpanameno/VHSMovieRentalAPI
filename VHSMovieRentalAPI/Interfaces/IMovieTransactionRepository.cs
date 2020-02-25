using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Interfaces
{
    public interface IMovieTransactionRepository : IRepository<MovieTransaction>
    {

        MovieTransaction GetWithDetailsById(int iTransactionID);
        int CreateTransaction(MovieTransaction oMovieTransaction);
    }
}
