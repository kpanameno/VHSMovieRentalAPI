using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class TransactionTypeRepository : DBContextRepository<TransactionType>, ITransactionTypeRepository
    {

        public TransactionTypeRepository(VHSMovieRentalDBContext context) : base(context)
        {
        }
    }
}
