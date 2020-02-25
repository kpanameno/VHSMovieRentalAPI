using VHSMovieRentalAPI.Interfaces;
using VHSMovieRentalAPI.Models;

namespace VHSMovieRentalAPI.Repositories
{
    public class MoviePriceLogRepository : DBContextRepository<MoviePriceLog>, IMoviePriceLogRepository
    {
        public MoviePriceLogRepository(VHSMovieRentalDBContext context) : base(context)
        {

        }
    }
}
